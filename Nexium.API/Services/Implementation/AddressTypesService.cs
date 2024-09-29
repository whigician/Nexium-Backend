using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.AddressType;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services.Implementation;

public class AddressTypesService(
    AddressTypeMapper mapper,
    IAddressTypesRepository addressTypesRepository,
    SelectedLanguageService selectedLanguageService,
    TranslationMapper translationMapper) : IAddressTypesService
{
    public async Task<List<AddressTypeView>> GetAllAddressTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var addressTypes = await addressTypesRepository.GetAllAddressTypes(cancellationToken, selectedLanguage);
        return addressTypes.Select(x => new AddressTypeView
        {
            Id = x.Id,
            Label = x.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ?? x.Label
        }).ToList();
    }

    public async Task<AddressTypeView> GetSingleAddressTypeById(byte addressTypeId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var addressType =
            await addressTypesRepository.GetSingleAddressTypeById(addressTypeId, cancellationToken, selectedLanguage,
                true);
        if (addressType == null)
            throw new EntityNotFoundException(nameof(AddressType), nameof(addressTypeId), addressTypeId.ToString());
        return new AddressTypeView
        {
            Id = addressType.Id,
            Label =
                addressType.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ??
                addressType.Label
        };
    }

    public async Task<AddressTypeView> CreateAddressType(AddressTypeSave addressTypeToCreate,
        CancellationToken cancellationToken)
    {
        var addressType = mapper.MapToAddressType(addressTypeToCreate);
        var createdAddressType = await addressTypesRepository.CreateAddressType(addressType, cancellationToken);
        return mapper.MapToAddressTypeView(createdAddressType);
    }

    public async Task UpdateAddressType(byte addressTypeId, AddressTypeSave addressTypeToUpdate,
        CancellationToken cancellationToken)
    {
        var addressTypeUpdatedValues = mapper.MapToAddressType(addressTypeToUpdate);
        var existingAddressType =
            await addressTypesRepository.GetSingleAddressTypeById(addressTypeId, cancellationToken);
        if (existingAddressType == null)
            throw new EntityNotFoundException(nameof(AddressType), nameof(addressTypeId), addressTypeId.ToString());
        existingAddressType.Label = addressTypeUpdatedValues.Label;
        await addressTypesRepository.UpdateAddressType(existingAddressType, cancellationToken);
    }

    public async Task DeleteAddressType(byte addressTypeId, CancellationToken cancellationToken)
    {
        var existingAddressType =
            await addressTypesRepository.GetSingleAddressTypeById(addressTypeId, cancellationToken);
        if (existingAddressType == null)
            throw new EntityNotFoundException(nameof(AddressType), nameof(addressTypeId),
                addressTypeId.ToString());
        try
        {
            await addressTypesRepository.DeleteAddressType(existingAddressType, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(AddressType), nameof(addressTypeId), addressTypeId.ToString());
        }
    }

    public async Task<List<TranslationView>> GetASingleAddressTypeTranslations(byte addressTypeId,
        CancellationToken cancellationToken)
    {
        return translationMapper.MapToAddressTypeTranslationViewList(
            await addressTypesRepository.GetASingleAddressTypeTranslations(addressTypeId, cancellationToken, true));
    }

    public async Task UpdateASingleAddressTypeTranslations(byte addressTypeId,
        List<TranslationSave> addressTypeTranslationsToSave, CancellationToken cancellationToken)
    {
        var existingTranslations =
            await addressTypesRepository.GetASingleAddressTypeTranslations(addressTypeId, cancellationToken);
        var translationsToCreate =
            translationMapper.MapToAddressTypeTranslationList(addressTypeTranslationsToSave
                .Where(x => x.Id is 0 or null).ToList());
        translationsToCreate.ForEach(t => t.AddressTypeId = addressTypeId);
        var translationsUpdateInformation = addressTypeTranslationsToSave
            .Where(x => existingTranslations.Any(i => i.Id == x.Id)).ToList();
        var translationsToUpdate = translationsUpdateInformation.Select(x =>
        {
            var translationToBeUpdated = existingTranslations.First(et => et.Id == x.Id);
            if (translationToBeUpdated.LanguageCode == x.LanguageCode &&
                translationToBeUpdated.TranslatedLabel == x.TranslatedLabel) return null;
            translationToBeUpdated.LanguageCode = x.LanguageCode;
            translationToBeUpdated.TranslatedLabel = x.TranslatedLabel;
            return translationToBeUpdated;
        }).Where(x => x != null).ToList();
        var translationsToDelete = existingTranslations
            .Where(et => addressTypeTranslationsToSave.All(nt => nt.Id != et.Id && nt.Id != null && nt.Id != 0))
            .ToList();

        await SaveTranslations(translationsToCreate, translationsToUpdate, translationsToDelete, cancellationToken);
    }

    private async Task SaveTranslations(List<AddressTypeTranslation> translationsToCreate,
        List<AddressTypeTranslation> translationsToUpdate, List<AddressTypeTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        try
        {
            if (translationsToCreate.Count != 0)
                await addressTypesRepository.AddTranslations(translationsToCreate, cancellationToken);

            if (translationsToUpdate.Count != 0)
                await addressTypesRepository.UpdateTranslations(translationsToUpdate, cancellationToken);
        }
        catch (UniqueConstraintException ex)
        {
            throw new LanguageCodeAlreadyExists("", nameof(AddressType));
        }

        if (translationsToDelete.Count != 0)
            await addressTypesRepository.RemoveTranslations(translationsToDelete, cancellationToken);
    }
}