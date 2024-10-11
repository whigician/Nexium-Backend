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
            .Include(i => i.Translations.Where(t => t.LanguageCode == selectedLanguage))
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TargetMarket> GetSingleTargetMarketById(byte targetMarketId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        var fetchQuery =
            dbContext.TargetMarkets.Include(x => x.Translations.Where(t => t.LanguageCode == selectedLanguage));
        if (forView)
            return await fetchQuery.AsNoTracking().FirstOrDefaultAsync(x => x.Id == targetMarketId, cancellationToken);
        return await fetchQuery.FirstOrDefaultAsync(x => x.Id == targetMarketId, cancellationToken);
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

    public Task<List<TargetMarketTranslation>> GetASingleTargetMarketTranslations(byte targetMarketId,
        CancellationToken cancellationToken, bool forView = false)
    {
        var fetchQuery = dbContext.TargetMarketsTranslations
            .Where(x => x.TargetMarketId == targetMarketId);
        return forView
            ? fetchQuery.AsNoTracking().ToListAsync(cancellationToken)
            : fetchQuery.ToListAsync(cancellationToken);
    }

    public async Task AddTranslations(List<TargetMarketTranslation> translationsToCreate,
        CancellationToken cancellationToken)
    {
        await dbContext.TargetMarketsTranslations.AddRangeAsync(translationsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslations(List<TargetMarketTranslation> translationsToUpdate,
        CancellationToken cancellationToken)
    {
        dbContext.UpdateRange(translationsToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTranslations(List<TargetMarketTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        dbContext.RemoveRange(translationsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}