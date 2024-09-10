using Nexium.API.TransferObjects.Industry;
using Nexium.API.TransferObjects.IndustryTranslation;

namespace Nexium.API.Services;

public interface IIndustriesService
{
    public Task<List<IndustryView>> GetAllIndustries(CancellationToken cancellationToken);
    public Task<IndustryView> GetSingleIndustryById(short industryId, CancellationToken cancellationToken);
    public Task<IndustryView> CreateIndustry(IndustrySave industryToCreate, CancellationToken cancellationToken);
    public Task UpdateIndustry(short industryId, IndustrySave industryToUpdate, CancellationToken cancellationToken);
    public Task DeleteIndustry(short industryId, CancellationToken cancellationToken);

    Task<List<IndustryTranslationView>> GetASingleIndustryTranslations(short industryId,
        CancellationToken cancellationToken);

    Task UpdateASingleIndustryTranslations(short industryId, List<IndustryTranslationSave> industryTranslationToSave,
        CancellationToken cancellationToken);
}