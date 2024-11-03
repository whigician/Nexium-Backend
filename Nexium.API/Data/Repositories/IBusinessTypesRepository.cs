using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IBusinessTypesRepository
{
    public Task<List<BusinessType>>
        GetAllBusinessTypes(CancellationToken cancellationToken, string selectedLanguage);

    public Task<BusinessType> GetSingleBusinessTypeById(byte businessTypeId, CancellationToken cancellationToken,
        string selectedLanguage = "fr",
        bool forView = false);

    Task<BusinessType> CreateBusinessType(BusinessType businessType, CancellationToken cancellationToken);
    Task UpdateBusinessType(BusinessType businessType, CancellationToken cancellationToken);
    Task DeleteBusinessType(BusinessType existingBusinessType, CancellationToken cancellationToken);
}