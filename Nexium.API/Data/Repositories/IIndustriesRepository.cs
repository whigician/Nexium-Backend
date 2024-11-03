using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IIndustriesRepository
{
    public Task<List<Industry>>
        GetAllIndustries(CancellationToken cancellationToken, string selectedLanguage);

    public Task<Industry> GetSingleIndustryById(short industryId, CancellationToken cancellationToken,
        string selectedLanguage = "fr",
        bool forView = false);

    Task<Industry> CreateIndustry(Industry industry, CancellationToken cancellationToken);
    Task UpdateIndustry(Industry industry, CancellationToken cancellationToken);
    Task DeleteIndustry(Industry existingIndustry, CancellationToken cancellationToken);
}