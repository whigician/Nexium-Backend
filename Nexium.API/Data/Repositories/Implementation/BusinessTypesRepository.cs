using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class BusinessTypesRepository(NexiumDbContext dbContext)
    : IBusinessTypesRepository
{
    public Task<List<BusinessType>> GetAllBusinessTypes(CancellationToken cancellationToken, string selectedLanguage = "fr-FR")
    {
        return dbContext.BusinessTypes
            .Include(i => i.Translations.Where(t => t.LanguageCode == selectedLanguage))
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<BusinessType> GetSingleBusinessTypeById(byte businessTypeId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        var fetchQuery =
            dbContext.BusinessTypes.Include(x => x.Translations.Where(t => t.LanguageCode == selectedLanguage));
        if (forView)
            return await fetchQuery.AsNoTracking().FirstOrDefaultAsync(x => x.Id == businessTypeId, cancellationToken);
        return await fetchQuery.FirstOrDefaultAsync(x => x.Id == businessTypeId, cancellationToken);
    }

    public async Task<BusinessType> CreateBusinessType(BusinessType businessType, CancellationToken cancellationToken)
    {
        dbContext.BusinessTypes.Add(businessType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return businessType;
    }

    public async Task UpdateBusinessType(BusinessType businessType, CancellationToken cancellationToken)
    {
        dbContext.BusinessTypes.Update(businessType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBusinessType(BusinessType existingBusinessType, CancellationToken cancellationToken)
    {
        dbContext.BusinessTypes.Remove(existingBusinessType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<BusinessTypeTranslation>> GetASingleBusinessTypeTranslations(byte businessTypeId,
        CancellationToken cancellationToken, bool forView = false)
    {
        var fetchQuery = dbContext.BusinessTypesTranslations
            .Where(x => x.BusinessTypeId == businessTypeId);
        return forView
            ? fetchQuery.AsNoTracking().ToListAsync(cancellationToken)
            : fetchQuery.ToListAsync(cancellationToken);
    }

    public async Task AddTranslations(List<BusinessTypeTranslation> translationsToCreate,
        CancellationToken cancellationToken)
    {
        await dbContext.BusinessTypesTranslations.AddRangeAsync(translationsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslations(List<BusinessTypeTranslation> translationsToUpdate,
        CancellationToken cancellationToken)
    {
        dbContext.UpdateRange(translationsToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTranslations(List<BusinessTypeTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        dbContext.RemoveRange(translationsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}