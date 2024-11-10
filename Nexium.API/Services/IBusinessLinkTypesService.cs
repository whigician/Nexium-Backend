using Nexium.API.TransferObjects;
using Nexium.API.TransferObjects.BusinessLinkType;

namespace Nexium.API.Services;

public interface IBusinessLinkTypesService
{
    public Task<List<BusinessLinkTypeView>> GetAllBusinessLinkTypes(CancellationToken cancellationToken);

    public Task<BusinessLinkTypeView> GetSingleBusinessLinkTypeById(byte businessLinkTypeId,
        CancellationToken cancellationToken);

    public Task<BusinessLinkTypeView> CreateBusinessLinkType(BusinessLinkTypeSave businessLinkTypeToCreate,
        CancellationToken cancellationToken);

    public Task UpdateBusinessLinkType(byte businessLinkTypeId, BusinessLinkTypeSave businessLinkTypeToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteBusinessLinkType(byte businessLinkTypeId, CancellationToken cancellationToken);
    Task<UploadResponse> UploadLogo(byte businessLinkTypeId, IFormFile logo);
}