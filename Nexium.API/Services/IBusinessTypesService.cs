using Nexium.API.TransferObjects.BusinessType;

namespace Nexium.API.Services;

public interface IBusinessTypesService
{
    public Task<List<BusinessTypeView>> GetAllBusinessTypes(CancellationToken cancellationToken);
    public Task<BusinessTypeView> GetSingleBusinessTypeById(byte businessTypeId, CancellationToken cancellationToken);

    public Task<BusinessTypeView> CreateBusinessType(BusinessTypeSave businessTypeToCreate,
        CancellationToken cancellationToken);

    public Task UpdateBusinessType(byte businessTypeId, BusinessTypeSave businessTypeToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteBusinessType(byte businessTypeId, CancellationToken cancellationToken);
}