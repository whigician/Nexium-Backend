using Nexium.API.TransferObjects.BusinessStatus;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services;

public interface IBusinessStatusesService
{
    public Task<List<BusinessStatusView>> GetAllBusinessStatuses(CancellationToken cancellationToken);

    public Task<BusinessStatusView> GetSingleBusinessStatusById(byte businessStatusId,
        CancellationToken cancellationToken);

    public Task<BusinessStatusView> CreateBusinessStatus(BusinessStatusSave businessStatusToCreate,
        CancellationToken cancellationToken);

    public Task UpdateBusinessStatus(byte businessStatusId, BusinessStatusSave businessStatusToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteBusinessStatus(byte businessStatusId, CancellationToken cancellationToken);

    Task<List<TranslationView>> GetASingleBusinessStatusTranslations(byte businessStatusId,
        CancellationToken cancellationToken);

    Task UpdateASingleBusinessStatusTranslations(byte businessStatusId,
        List<TranslationSave> businessStatusTranslationsToSave,
        CancellationToken cancellationToken);
}