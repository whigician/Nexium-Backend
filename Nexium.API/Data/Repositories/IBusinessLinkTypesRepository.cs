using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IBusinessLinkTypesRepository
{
    public Task<List<BusinessLinkType>>
        GetAllBusinessLinkTypes(CancellationToken cancellationToken);

    public Task<BusinessLinkType> GetSingleBusinessLinkTypeById(byte businessLinkTypeId,
        CancellationToken cancellationToken,
        bool forView = false);

    Task<BusinessLinkType> CreateBusinessLinkType(BusinessLinkType businessLinkType,
        CancellationToken cancellationToken);

    Task UpdateBusinessLinkType(BusinessLinkType businessLinkType, CancellationToken cancellationToken);
    Task DeleteBusinessLinkType(BusinessLinkType existingBusinessLinkType, CancellationToken cancellationToken);
}