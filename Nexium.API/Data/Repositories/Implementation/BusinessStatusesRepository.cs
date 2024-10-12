using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class BusinessStatusesRepository(NexiumDbContext dbContext)
    : IBusinessStatusesRepository
{
    public Task<List<BusinessStatus>> GetAllBusinessStatuses(CancellationToken cancellationToken,
        string selectedLanguage)
    {
        return dbContext.BusinessStatuses
            .Include(i => i.Translations.Where(t => t.LanguageCode == selectedLanguage))
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<BusinessStatus> GetSingleBusinessStatusById(byte businessStatusId,
        CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        var fetchQuery =
            dbContext.BusinessStatuses.Include(x => x.Translations.Where(t => t.LanguageCode == selectedLanguage));
        if (forView)
            return await fetchQuery.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == businessStatusId, cancellationToken);
        return await fetchQuery.FirstOrDefaultAsync(x => x.Id == businessStatusId, cancellationToken);
    }

    public async Task<BusinessStatus> CreateBusinessStatus(BusinessStatus businessStatus,
        CancellationToken cancellationToken)
    {
        dbContext.BusinessStatuses.Add(businessStatus);
        await dbContext.SaveChangesAsync(cancellationToken);
        return businessStatus;
    }

    public async Task UpdateBusinessStatus(BusinessStatus businessStatus, CancellationToken cancellationToken)
    {
        dbContext.BusinessStatuses.Update(businessStatus);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBusinessStatus(BusinessStatus existingBusinessStatus, CancellationToken cancellationToken)
    {
        dbContext.BusinessStatuses.Remove(existingBusinessStatus);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<BusinessStatusTranslation>> GetASingleBusinessStatusTranslations(byte businessStatusId,
        CancellationToken cancellationToken, bool forView = false)
    {
        var fetchQuery = dbContext.BusinessStatusesTranslations
            .Where(x => x.BusinessStatusId == businessStatusId);
        return forView
            ? fetchQuery.AsNoTracking().ToListAsync(cancellationToken)
            : fetchQuery.ToListAsync(cancellationToken);
    }

    public async Task AddTranslations(List<BusinessStatusTranslation> translationsToCreate,
        CancellationToken cancellationToken)
    {
        await dbContext.BusinessStatusesTranslations.AddRangeAsync(translationsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslations(List<BusinessStatusTranslation> translationsToUpdate,
        CancellationToken cancellationToken)
    {
        dbContext.UpdateRange(translationsToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTranslations(List<BusinessStatusTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        dbContext.RemoveRange(translationsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}