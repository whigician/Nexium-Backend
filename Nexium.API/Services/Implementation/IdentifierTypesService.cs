using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.IdentifierType;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services.Implementation;

public class IdentifierTypesService(
    IdentifierTypeMapper mapper,
    IIdentifierTypesRepository identifierTypesRepository,
    SelectedLanguageService selectedLanguageService,
    TranslationMapper translationMapper) : IIdentifierTypesService
{
    public async Task<List<IdentifierTypeView>> GetAllIdentifierTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var identifierTypes =
            await identifierTypesRepository.GetAllIdentifierTypes(cancellationToken, selectedLanguage);
        return identifierTypes.Select(x => new IdentifierTypeView
        {
            Id = x.Id,
            Label = x.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ?? x.Label
        }).ToList();
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
            Label =
                identifierType.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ??
                identifierType.Label
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

    public async Task<List<TranslationView>> GetASingleIdentifierTypeTranslations(byte identifierTypeId,
        CancellationToken cancellationToken)
    {
        return translationMapper.MapToIdentifierTypeTranslationViewList(
            await identifierTypesRepository.GetASingleIdentifierTypeTranslations(identifierTypeId, cancellationToken,
                true));
    }

    public async Task UpdateASingleIdentifierTypeTranslations(byte identifierTypeId,
        List<TranslationSave> identifierTypeTranslationsToSave, CancellationToken cancellationToken)
    {
        var existingTranslations =
            await identifierTypesRepository.GetASingleIdentifierTypeTranslations(identifierTypeId, cancellationToken);
        var translationsToCreate =
            translationMapper.MapToIdentifierTypeTranslationList(identifierTypeTranslationsToSave
                .Where(x => x.Id is 0 or null).ToList());
        translationsToCreate.ForEach(t => t.IdentifierTypeId = identifierTypeId);
        var translationsUpdateInformation = identifierTypeTranslationsToSave
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
            .Where(et => identifierTypeTranslationsToSave.All(nt => nt.Id != et.Id && nt.Id != null && nt.Id != 0))
            .ToList();

        await SaveTranslations(translationsToCreate, translationsToUpdate, translationsToDelete, cancellationToken);
    }

    private async Task SaveTranslations(List<IdentifierTypeTranslation> translationsToCreate,
        List<IdentifierTypeTranslation> translationsToUpdate, List<IdentifierTypeTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        try
        {
            if (translationsToCreate.Count != 0)
                await identifierTypesRepository.AddTranslations(translationsToCreate, cancellationToken);

            if (translationsToUpdate.Count != 0)
                await identifierTypesRepository.UpdateTranslations(translationsToUpdate, cancellationToken);
        }
        catch (UniqueConstraintException ex)
        {
            throw new LanguageCodeAlreadyExists("", nameof(IdentifierType));
        }

        if (translationsToDelete.Count != 0)
            await identifierTypesRepository.RemoveTranslations(translationsToDelete, cancellationToken);
    }
}