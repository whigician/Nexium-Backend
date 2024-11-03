using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class TargetMarketsRepository(NexiumDbContext dbContext)
    : ITargetMarketsRepository
{
    public Task<List<TargetMarket>> GetAllTargetMarkets(CancellationToken cancellationToken,
        string selectedLanguage)
    {
        return dbContext.TargetMarkets
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TargetMarket> GetSingleTargetMarketById(byte targetMarketId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        if (forView)
            return await dbContext.TargetMarkets.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == targetMarketId, cancellationToken);
        return await dbContext.TargetMarkets.FirstOrDefaultAsync(x => x.Id == targetMarketId, cancellationToken);
    }

    public async Task<TargetMarket> CreateTargetMarket(TargetMarket targetMarket, CancellationToken cancellationToken)
    {
        dbContext.TargetMarkets.Add(targetMarket);
        await dbContext.SaveChangesAsync(cancellationToken);
        return targetMarket;
    }

    public async Task UpdateTargetMarket(TargetMarket targetMarket, CancellationToken cancellationToken)
    {
        dbContext.TargetMarkets.Update(targetMarket);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTargetMarket(TargetMarket existingTargetMarket, CancellationToken cancellationToken)
    {
        dbContext.TargetMarkets.Remove(existingTargetMarket);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}