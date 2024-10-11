using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IBusinessStatusesRepository
{
    public Task<List<BusinessStatus>>
        GetAllBusinessStatuses(CancellationToken cancellationToken, string selectedLanguage);

    public Task<BusinessStatus> GetSingleBusinessStatusById(byte businessStatusId, CancellationToken cancellationToken,
        string selectedLanguage = "fr",
        bool forView = false);

    Task<BusinessStatus> CreateBusinessStatus(BusinessStatus businessStatus, CancellationToken cancellationToken);
    Task UpdateBusinessStatus(BusinessStatus businessStatus, CancellationToken cancellationToken);
    Task DeleteBusinessStatus(BusinessStatus existingBusinessStatus, CancellationToken cancellationToken);

    Task<List<BusinessStatusTranslation>> GetASingleBusinessStatusTranslations(byte businessStatusId,
        CancellationToken cancellationToken, bool forView = false);

    Task AddTranslations(List<BusinessStatusTranslation> translationsToCreate, CancellationToken cancellationToken);
    Task UpdateTranslations(List<BusinessStatusTranslation> translationsToUpdate, CancellationToken cancellationToken);
    Task RemoveTranslations(List<BusinessStatusTranslation> translationsToDelete, CancellationToken cancellationToken);
}