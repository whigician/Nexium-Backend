using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.ContactType;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services.Implementation;

public class ContactTypesService(
    ContactTypeMapper mapper,
    IContactTypesRepository contactTypesRepository,
    SelectedLanguageService selectedLanguageService,
    TranslationMapper translationMapper) : IContactTypesService
{
    public async Task<List<ContactTypeView>> GetAllContactTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var contactTypes = await contactTypesRepository.GetAllContactTypes(cancellationToken, selectedLanguage);
        return contactTypes.Select(x => new ContactTypeView
        {
            Id = x.Id,
            Label = x.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ?? x.Label
        }).ToList();
    }

    public async Task<ContactTypeView> GetSingleContactTypeById(byte contactTypeId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var contactType =
            await contactTypesRepository.GetSingleContactTypeById(contactTypeId, cancellationToken, selectedLanguage,
                true);
        if (contactType == null)
            throw new EntityNotFoundException(nameof(ContactType), nameof(contactTypeId), contactTypeId.ToString());
        return new ContactTypeView
        {
            Id = contactType.Id,
            Label =
                contactType.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ??
                contactType.Label
        };
    }

    public async Task<ContactTypeView> CreateContactType(ContactTypeSave contactTypeToCreate,
        CancellationToken cancellationToken)
    {
        var contactType = mapper.MapToContactType(contactTypeToCreate);
        var createdContactType = await contactTypesRepository.CreateContactType(contactType, cancellationToken);
        return mapper.MapToContactTypeView(createdContactType);
    }

    public async Task UpdateContactType(byte contactTypeId, ContactTypeSave contactTypeToUpdate,
        CancellationToken cancellationToken)
    {
        var contactTypeUpdatedValues = mapper.MapToContactType(contactTypeToUpdate);
        var existingContactType =
            await contactTypesRepository.GetSingleContactTypeById(contactTypeId, cancellationToken);
        if (existingContactType == null)
            throw new EntityNotFoundException(nameof(ContactType), nameof(contactTypeId), contactTypeId.ToString());
        existingContactType.Label = contactTypeUpdatedValues.Label;
        await contactTypesRepository.UpdateContactType(existingContactType, cancellationToken);
    }

    public async Task DeleteContactType(byte contactTypeId, CancellationToken cancellationToken)
    {
        var existingContactType =
            await contactTypesRepository.GetSingleContactTypeById(contactTypeId, cancellationToken);
        if (existingContactType == null)
            throw new EntityNotFoundException(nameof(ContactType), nameof(contactTypeId),
                contactTypeId.ToString());
        try
        {
            await contactTypesRepository.DeleteContactType(existingContactType, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(ContactType), nameof(contactTypeId),
                contactTypeId.ToString());
        }
    }

    public async Task<List<TranslationView>> GetASingleContactTypeTranslations(byte contactTypeId,
        CancellationToken cancellationToken)
    {
        return translationMapper.MapToContactTypeTranslationViewList(
            await contactTypesRepository.GetASingleContactTypeTranslations(contactTypeId, cancellationToken, true));
    }

    public async Task UpdateASingleContactTypeTranslations(byte contactTypeId,
        List<TranslationSave> contactTypeTranslationsToSave, CancellationToken cancellationToken)
    {
        var existingTranslations =
            await contactTypesRepository.GetASingleContactTypeTranslations(contactTypeId, cancellationToken);
        var translationsToCreate =
            translationMapper.MapToContactTypeTranslationList(contactTypeTranslationsToSave
                .Where(x => x.Id is 0 or null).ToList());
        translationsToCreate.ForEach(t => t.ContactTypeId = contactTypeId);
        var translationsUpdateInformation = contactTypeTranslationsToSave
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
            .Where(et => contactTypeTranslationsToSave.All(nt => nt.Id != et.Id && nt.Id != null && nt.Id != 0))
            .ToList();

        await SaveTranslations(translationsToCreate, translationsToUpdate, translationsToDelete, cancellationToken);
    }

    private async Task SaveTranslations(List<ContactTypeTranslation> translationsToCreate,
        List<ContactTypeTranslation> translationsToUpdate, List<ContactTypeTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        try
        {
            if (translationsToCreate.Count != 0)
                await contactTypesRepository.AddTranslations(translationsToCreate, cancellationToken);

            if (translationsToUpdate.Count != 0)
                await contactTypesRepository.UpdateTranslations(translationsToUpdate, cancellationToken);
        }
        catch (UniqueConstraintException ex)
        {
            throw new LanguageCodeAlreadyExists("", nameof(ContactType));
        }

        if (translationsToDelete.Count != 0)
            await contactTypesRepository.RemoveTranslations(translationsToDelete, cancellationToken);
    }
}