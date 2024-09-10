using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class IndustriesRepository(NexiumDbContext dbContext)
    : IIndustriesRepository
{
    public Task<List<Industry>> GetAllIndustries(CancellationToken cancellationToken, string selectedLanguage = "fr-FR")
    {
        return dbContext.Industries
            .Include(i => i.Translations.Where(t => t.LanguageCode == selectedLanguage))
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Industry> GetSingleIndustryById(short industryId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        var fetchQuery =
            dbContext.Industries.Include(x => x.Translations.Where(t => t.LanguageCode == selectedLanguage));
        if (forView)
            return await fetchQuery.AsNoTracking().FirstOrDefaultAsync(x => x.Id == industryId, cancellationToken);
        return await fetchQuery.FirstOrDefaultAsync(x => x.Id == industryId, cancellationToken);
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

    public Task<List<IndustryTranslation>> GetASingleIndustryTranslations(short industryId,
        CancellationToken cancellationToken, bool forView = false)
    {
        var fetchQuery = dbContext.IndustryTranslations
            .Where(x => x.IndustryId == industryId);
        return forView
            ? fetchQuery.AsNoTracking().ToListAsync(cancellationToken)
            : fetchQuery.ToListAsync(cancellationToken);
    }

    public async Task AddTranslations(List<IndustryTranslation> translationsToCreate,
        CancellationToken cancellationToken)
    {
        await dbContext.IndustryTranslations.AddRangeAsync(translationsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslations(List<IndustryTranslation> translationsToUpdate,
        CancellationToken cancellationToken)
    {
        dbContext.UpdateRange(translationsToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTranslations(List<IndustryTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        dbContext.RemoveRange(translationsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}