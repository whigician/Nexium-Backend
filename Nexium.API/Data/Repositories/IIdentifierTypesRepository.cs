using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IIdentifierTypesRepository
{
    public Task<List<IdentifierType>>
        GetAllIdentifierTypes(CancellationToken cancellationToken, string selectedLanguage = "fr-FR");

    public Task<IdentifierType> GetSingleIdentifierTypeById(byte identifierTypeId, CancellationToken cancellationToken,
        string selectedLanguage = "fr-FR",
        bool forView = false);

    Task<IdentifierType> CreateIdentifierType(IdentifierType identifierType, CancellationToken cancellationToken);
    Task UpdateIdentifierType(IdentifierType identifierType, CancellationToken cancellationToken);
    Task DeleteIdentifierType(IdentifierType existingIdentifierType, CancellationToken cancellationToken);

    Task<List<IdentifierTypeTranslation>> GetASingleIdentifierTypeTranslations(byte identifierTypeId,
        CancellationToken cancellationToken, bool forView = false);

    Task AddTranslations(List<IdentifierTypeTranslation> translationsToCreate, CancellationToken cancellationToken);
    Task UpdateTranslations(List<IdentifierTypeTranslation> translationsToUpdate, CancellationToken cancellationToken);
    Task RemoveTranslations(List<IdentifierTypeTranslation> translationsToDelete, CancellationToken cancellationToken);
}