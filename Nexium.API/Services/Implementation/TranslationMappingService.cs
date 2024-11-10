using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services.Implementation;

public class TranslationMappingService(
    ITranslationMappingRepository translationMappingRepository,
    TranslationMapper translationMapper) : ITranslationMappingService
{
    public async Task UpdateASingleAddressTypeTranslations(string entityTypeName, long entityId, string attributeName,
        List<TranslationMappingSave> addressTypeTranslationsToSave, CancellationToken cancellationToken)
    {
        var existingTranslations =
            await translationMappingRepository.GetASingleEntityTranslationsForAttribute(entityTypeName, entityId,
                attributeName, cancellationToken);
        var translationsToCreate =
            translationMapper.MapToTranslationMappingList(addressTypeTranslationsToSave
                .Where(x => x.Id is 0 or null).ToList());
        translationsToCreate.ForEach(t =>
        {
            t.EntityId = entityId;
            t.EntityTypeName = entityTypeName;
            t.AttributeName = attributeName;
        });
        var translationsUpdateInformation = addressTypeTranslationsToSave
            .Where(x => existingTranslations.Any(i => i.Id == x.Id)).ToList();
        var translationsToUpdate = translationsUpdateInformation.Select(x =>
        {
            var translationToBeUpdated = existingTranslations.First(et => et.Id == x.Id);
            if (translationToBeUpdated.LanguageCode == x.LanguageCode &&
                translationToBeUpdated.TranslatedText == x.TranslatedText) return null;
            translationToBeUpdated.LanguageCode = x.LanguageCode;
            translationToBeUpdated.TranslatedText = x.TranslatedText;
            return translationToBeUpdated;
        }).Where(x => x != null).ToList();
        var translationsToDelete = existingTranslations
            .Where(et => addressTypeTranslationsToSave.All(nt => nt.Id != et.Id && nt.Id != null && nt.Id != 0))
            .ToList();

        await SaveTranslations(translationsToCreate, translationsToUpdate, translationsToDelete, cancellationToken);
    }

    public async Task<List<TranslationMappingView>> GetASingleEntityTranslations(long entityId, string entityName,
        string attributeName,
        CancellationToken cancellationToken)
    {
        return translationMapper.MapToTranslationMappingViewList(
            await translationMappingRepository.GetASingleEntityTranslationsForAttribute(entityName, entityId,
                attributeName,
                cancellationToken));
    }

    private async Task SaveTranslations(List<TranslationMapping> translationsToCreate,
        List<TranslationMapping> translationsToUpdate, List<TranslationMapping> translationsToDelete,
        CancellationToken cancellationToken)
    {
        try
        {
            if (translationsToCreate.Count != 0)
                await translationMappingRepository.AddTranslations(translationsToCreate, cancellationToken);

            if (translationsToUpdate.Count != 0)
                await translationMappingRepository.UpdateTranslations(translationsToUpdate, cancellationToken);
        }
        catch (UniqueConstraintException)
        {
            throw new LanguageCodeAlreadyExists("", nameof(AddressType));
        }

        if (translationsToDelete.Count != 0)
            await translationMappingRepository.RemoveTranslations(translationsToDelete, cancellationToken);
    }
}