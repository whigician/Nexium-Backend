using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IBusinessTypesRepository
{
    public Task<List<BusinessType>>
        GetAllBusinessTypes(CancellationToken cancellationToken, string selectedLanguage = "fr-FR");

    public Task<BusinessType> GetSingleBusinessTypeById(byte businessTypeId, CancellationToken cancellationToken,
        string selectedLanguage = "fr-FR",
        bool forView = false);

    Task<BusinessType> CreateBusinessType(BusinessType businessType, CancellationToken cancellationToken);
    Task UpdateBusinessType(BusinessType businessType, CancellationToken cancellationToken);
    Task DeleteBusinessType(BusinessType existingBusinessType, CancellationToken cancellationToken);

    Task<List<BusinessTypeTranslation>> GetASingleBusinessTypeTranslations(byte businessTypeId,
        CancellationToken cancellationToken, bool forView = false);

    Task AddTranslations(List<BusinessTypeTranslation> translationsToCreate, CancellationToken cancellationToken);
    Task UpdateTranslations(List<BusinessTypeTranslation> translationsToUpdate, CancellationToken cancellationToken);
    Task RemoveTranslations(List<BusinessTypeTranslation> translationsToDelete, CancellationToken cancellationToken);
}