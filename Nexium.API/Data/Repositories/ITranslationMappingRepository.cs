using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface ITranslationMappingRepository
{
    Task<TranslationMapping> GetSingleEntityTranslationForAttribute(string entityName, long entityId,
        string attributeName, string languageCode, CancellationToken cancellationToken);

    Task<List<TranslationMapping>> GetASingleEntityTranslationsForAttribute(string entityTypeName, long entityId,
        string attributeName, CancellationToken cancellationToken);

    Task AddTranslations(List<TranslationMapping> translationsToCreate, CancellationToken cancellationToken);
    Task UpdateTranslations(List<TranslationMapping> translationsToUpdate, CancellationToken cancellationToken);
    Task RemoveTranslations(List<TranslationMapping> translationsToDelete, CancellationToken cancellationToken);
}