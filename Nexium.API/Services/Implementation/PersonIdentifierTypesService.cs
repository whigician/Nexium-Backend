using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.PersonIdentifierType;

namespace Nexium.API.Services.Implementation;

public class PersonIdentifierTypesService(
    PersonIdentifierTypeMapper mapper,
    IPersonIdentifierTypesRepository personIdentifierTypesRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : IPersonIdentifierTypesService
{
    public async Task<List<PersonIdentifierTypeView>> GetAllPersonIdentifierTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var personIdentifierTypes =
            await personIdentifierTypesRepository.GetAllPersonIdentifierTypes(cancellationToken);

        var personIdentifierTypeViews = new List<PersonIdentifierTypeView>();
        foreach (var x in personIdentifierTypes)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "PersonIdentifierType", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            personIdentifierTypeViews.Add(new PersonIdentifierTypeView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return personIdentifierTypeViews;
    }

    public async Task<PersonIdentifierTypeView> GetSinglePersonIdentifierTypeById(byte personIdentifierTypeId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var personIdentifierType =
            await personIdentifierTypesRepository.GetSinglePersonIdentifierTypeById(personIdentifierTypeId,
                cancellationToken,
                true);
        if (personIdentifierType == null)
            throw new EntityNotFoundException(nameof(PersonIdentifierType), nameof(personIdentifierTypeId),
                personIdentifierTypeId.ToString());
        return new PersonIdentifierTypeView
        {
            Id = personIdentifierType.Id,
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("PersonIdentifierType",
                        personIdentifierType.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ??
                    personIdentifierType.Label
        };
    }

    public async Task<PersonIdentifierTypeView> CreatePersonIdentifierType(
        PersonIdentifierTypeSave personIdentifierTypeToCreate,
        CancellationToken cancellationToken)
    {
        var personIdentifierType = mapper.MapToPersonIdentifierType(personIdentifierTypeToCreate);
        var createdPersonIdentifierType =
            await personIdentifierTypesRepository.CreatePersonIdentifierType(personIdentifierType, cancellationToken);
        return mapper.MapToPersonIdentifierTypeView(createdPersonIdentifierType);
    }

    public async Task UpdatePersonIdentifierType(byte personIdentifierTypeId,
        PersonIdentifierTypeSave personIdentifierTypeToUpdate,
        CancellationToken cancellationToken)
    {
        var personIdentifierTypeUpdatedValues = mapper.MapToPersonIdentifierType(personIdentifierTypeToUpdate);
        var existingPersonIdentifierType =
            await personIdentifierTypesRepository.GetSinglePersonIdentifierTypeById(personIdentifierTypeId,
                cancellationToken);
        if (existingPersonIdentifierType == null)
            throw new EntityNotFoundException(nameof(PersonIdentifierType), nameof(personIdentifierTypeId),
                personIdentifierTypeId.ToString());
        existingPersonIdentifierType.Label = personIdentifierTypeUpdatedValues.Label;
        await personIdentifierTypesRepository.UpdatePersonIdentifierType(existingPersonIdentifierType,
            cancellationToken);
    }

    public async Task DeletePersonIdentifierType(byte personIdentifierTypeId, CancellationToken cancellationToken)
    {
        var existingPersonIdentifierType =
            await personIdentifierTypesRepository.GetSinglePersonIdentifierTypeById(personIdentifierTypeId,
                cancellationToken);
        if (existingPersonIdentifierType == null)
            throw new EntityNotFoundException(nameof(PersonIdentifierType), nameof(personIdentifierTypeId),
                personIdentifierTypeId.ToString());
        try
        {
            await personIdentifierTypesRepository.DeletePersonIdentifierType(existingPersonIdentifierType,
                cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(PersonIdentifierType), nameof(personIdentifierTypeId),
                personIdentifierTypeId.ToString());
        }
    }
}