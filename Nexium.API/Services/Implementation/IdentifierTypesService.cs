using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.IdentifierType;

namespace Nexium.API.Services.Implementation;

public class IdentifierTypesService(
    IdentifierTypeMapper mapper,
    IIdentifierTypesRepository identifierTypesRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : IIdentifierTypesService
{
    public async Task<List<IdentifierTypeView>> GetAllIdentifierTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var identifierTypes =
            await identifierTypesRepository.GetAllIdentifierTypes(cancellationToken, selectedLanguage);

        var identifierTypeViews = new List<IdentifierTypeView>();
        foreach (var x in identifierTypes)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "IdentifierType", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            identifierTypeViews.Add(new IdentifierTypeView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return identifierTypeViews;
    }

    public async Task<IdentifierTypeView> GetSingleIdentifierTypeById(byte identifierTypeId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var identifierType =
            await identifierTypesRepository.GetSingleIdentifierTypeById(identifierTypeId, cancellationToken,
                selectedLanguage,
                true);
        if (identifierType == null)
            throw new EntityNotFoundException(nameof(IdentifierType), nameof(identifierTypeId),
                identifierTypeId.ToString());
        return new IdentifierTypeView
        {
            Id = identifierType.Id,
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("IdentifierType",
                identifierType.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? identifierType.Label
        };
    }

    public async Task<IdentifierTypeView> CreateIdentifierType(IdentifierTypeSave identifierTypeToCreate,
        CancellationToken cancellationToken)
    {
        var identifierType = mapper.MapToIdentifierType(identifierTypeToCreate);
        var createdIdentifierType =
            await identifierTypesRepository.CreateIdentifierType(identifierType, cancellationToken);
        return mapper.MapToIdentifierTypeView(createdIdentifierType);
    }

    public async Task UpdateIdentifierType(byte identifierTypeId, IdentifierTypeSave identifierTypeToUpdate,
        CancellationToken cancellationToken)
    {
        var identifierTypeUpdatedValues = mapper.MapToIdentifierType(identifierTypeToUpdate);
        var existingIdentifierType =
            await identifierTypesRepository.GetSingleIdentifierTypeById(identifierTypeId, cancellationToken);
        if (existingIdentifierType == null)
            throw new EntityNotFoundException(nameof(IdentifierType), nameof(identifierTypeId),
                identifierTypeId.ToString());
        existingIdentifierType.Label = identifierTypeUpdatedValues.Label;
        await identifierTypesRepository.UpdateIdentifierType(existingIdentifierType, cancellationToken);
    }

    public async Task DeleteIdentifierType(byte identifierTypeId, CancellationToken cancellationToken)
    {
        var existingIdentifierType =
            await identifierTypesRepository.GetSingleIdentifierTypeById(identifierTypeId, cancellationToken);
        if (existingIdentifierType == null)
            throw new EntityNotFoundException(nameof(IdentifierType), nameof(identifierTypeId),
                identifierTypeId.ToString());
        try
        {
            await identifierTypesRepository.DeleteIdentifierType(existingIdentifierType, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(IdentifierType), nameof(identifierTypeId),
                identifierTypeId.ToString());
        }
    }
}