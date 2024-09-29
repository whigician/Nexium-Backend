using Nexium.API.TransferObjects.AddressType;
using Nexium.API.TransferObjects.Translation;

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

    Task<List<TranslationView>> GetASingleAddressTypeTranslations(byte addressTypeId,
        CancellationToken cancellationToken);

    Task UpdateASingleAddressTypeTranslations(byte addressTypeId,
        List<TranslationSave> addressTypeTranslationsToSave,
        CancellationToken cancellationToken);
}