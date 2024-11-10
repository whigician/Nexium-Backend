using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IAddressTypesRepository
{
    public Task<List<AddressType>>
        GetAllAddressTypes(CancellationToken cancellationToken);

    public Task<AddressType> GetSingleAddressTypeById(byte addressTypeId, CancellationToken cancellationToken,
        bool forView = false);

    Task<AddressType> CreateAddressType(AddressType addressType, CancellationToken cancellationToken);
    Task UpdateAddressType(AddressType addressType, CancellationToken cancellationToken);
    Task DeleteAddressType(AddressType existingAddressType, CancellationToken cancellationToken);
}