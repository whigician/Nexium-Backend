using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.ContactType;

namespace Nexium.API.Services.Implementation;

public class ContactTypesService(
    ContactTypeMapper mapper,
    IContactTypesRepository contactTypesRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : IContactTypesService
{
    public async Task<List<ContactTypeView>> GetAllContactTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessTypes = await contactTypesRepository.GetAllContactTypes(cancellationToken);

        var contactTypeViews = new List<ContactTypeView>();
        foreach (var x in businessTypes)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "ContactType", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            contactTypeViews.Add(new ContactTypeView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return contactTypeViews;
    }

    public async Task<ContactTypeView> GetSingleContactTypeById(byte contactTypeId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var contactType =
            await contactTypesRepository.GetSingleContactTypeById(contactTypeId, cancellationToken,
                true);
        if (contactType == null)
            throw new EntityNotFoundException(nameof(ContactType), nameof(contactTypeId), contactTypeId.ToString());
        return new ContactTypeView
        {
            Id = contactType.Id,
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("ContactType",
                contactType.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? contactType.Label
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
}