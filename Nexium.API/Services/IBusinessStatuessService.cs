using Nexium.API.TransferObjects.BusinessStatus;

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
}