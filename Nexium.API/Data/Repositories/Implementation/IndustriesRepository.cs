using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class IndustriesRepository(NexiumDbContext dbContext)
    : IIndustriesRepository
{
    public Task<List<Industry>> GetAllIndustries(CancellationToken cancellationToken, string selectedLanguage)
    {
        return dbContext.Industries
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Industry> GetSingleIndustryById(short industryId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        if (forView)
            return await dbContext.Industries.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == industryId, cancellationToken);
        return await dbContext.Industries.FirstOrDefaultAsync(x => x.Id == industryId, cancellationToken);
    }

    public async Task<Industry> CreateIndustry(Industry industry, CancellationToken cancellationToken)
    {
        dbContext.Industries.Add(industry);
        await dbContext.SaveChangesAsync(cancellationToken);
        return industry;
    }

    public async Task UpdateIndustry(Industry industry, CancellationToken cancellationToken)
    {
        dbContext.Industries.Update(industry);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteIndustry(Industry existingIndustry, CancellationToken cancellationToken)
    {
        dbContext.Industries.Remove(existingIndustry);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}