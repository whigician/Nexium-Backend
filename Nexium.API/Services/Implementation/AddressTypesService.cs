using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.AddressType;

namespace Nexium.API.Services.Implementation;

public class AddressTypesService(
    AddressTypeMapper mapper,
    IAddressTypesRepository addressTypesRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : IAddressTypesService
{
    public async Task<List<AddressTypeView>> GetAllAddressTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var addressTypes = await addressTypesRepository.GetAllAddressTypes(cancellationToken, selectedLanguage);

        var addressTypeViews = new List<AddressTypeView>();
        foreach (var x in addressTypes)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "AddressType", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            addressTypeViews.Add(new AddressTypeView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return addressTypeViews;
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
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("AddressType",
                addressType.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? addressType.Label
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
}