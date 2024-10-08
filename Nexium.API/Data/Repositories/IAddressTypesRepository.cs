using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IAddressTypesRepository
{
    public Task<List<AddressType>>
        GetAllAddressTypes(CancellationToken cancellationToken, string selectedLanguage = "fr-FR");

    public Task<AddressType> GetSingleAddressTypeById(byte addressTypeId, CancellationToken cancellationToken,
        string selectedLanguage = "fr",
        bool forView = false);

    Task<AddressType> CreateAddressType(AddressType addressType, CancellationToken cancellationToken);
    Task UpdateAddressType(AddressType addressType, CancellationToken cancellationToken);
    Task DeleteAddressType(AddressType existingAddressType, CancellationToken cancellationToken);

    Task<List<AddressTypeTranslation>> GetASingleAddressTypeTranslations(byte addressTypeId,
        CancellationToken cancellationToken, bool forView = false);

    Task AddTranslations(List<AddressTypeTranslation> translationsToCreate, CancellationToken cancellationToken);
    Task UpdateTranslations(List<AddressTypeTranslation> translationsToUpdate, CancellationToken cancellationToken);
    Task RemoveTranslations(List<AddressTypeTranslation> translationsToDelete, CancellationToken cancellationToken);
}