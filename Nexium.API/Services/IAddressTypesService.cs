using Nexium.API.TransferObjects.AddressType;

namespace Nexium.API.Services;

public interface IAddressTypesService
{
    public Task<List<AddressTypeView>> GetAllAddressTypes(CancellationToken cancellationToken);
    public Task<AddressTypeView> GetSingleAddressTypeById(byte addressTypeId, CancellationToken cancellationToken);

    public Task<AddressTypeView> CreateAddressType(AddressTypeSave addressTypeToCreate,
        CancellationToken cancellationToken);

    public Task UpdateAddressType(byte addressTypeId, AddressTypeSave addressTypeToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteAddressType(byte addressTypeId, CancellationToken cancellationToken);
}