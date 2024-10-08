using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class IdentifierTypesRepository(NexiumDbContext dbContext)
    : IIdentifierTypesRepository
{
    public Task<List<IdentifierType>> GetAllIdentifierTypes(CancellationToken cancellationToken,
        string selectedLanguage = "fr")
    {
        return dbContext.IdentifierTypes
            .Include(i => i.Translations.Where(t => t.LanguageCode == selectedLanguage))
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IdentifierType> GetSingleIdentifierTypeById(byte identifierTypeId,
        CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        var fetchQuery =
            dbContext.IdentifierTypes.Include(x => x.Translations.Where(t => t.LanguageCode == selectedLanguage));
        if (forView)
            return await fetchQuery.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == identifierTypeId, cancellationToken);
        return await fetchQuery.FirstOrDefaultAsync(x => x.Id == identifierTypeId, cancellationToken);
    }

    public async Task<IdentifierType> CreateIdentifierType(IdentifierType identifierType,
        CancellationToken cancellationToken)
    {
        dbContext.IdentifierTypes.Add(identifierType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return identifierType;
    }

    public async Task UpdateIdentifierType(IdentifierType identifierType, CancellationToken cancellationToken)
    {
        dbContext.IdentifierTypes.Update(identifierType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteIdentifierType(IdentifierType existingIdentifierType, CancellationToken cancellationToken)
    {
        dbContext.IdentifierTypes.Remove(existingIdentifierType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<IdentifierTypeTranslation>> GetASingleIdentifierTypeTranslations(byte identifierTypeId,
        CancellationToken cancellationToken, bool forView = false)
    {
        var fetchQuery = dbContext.IdentifierTypesTranslations
            .Where(x => x.IdentifierTypeId == identifierTypeId);
        return forView
            ? fetchQuery.AsNoTracking().ToListAsync(cancellationToken)
            : fetchQuery.ToListAsync(cancellationToken);
    }

    public async Task AddTranslations(List<IdentifierTypeTranslation> translationsToCreate,
        CancellationToken cancellationToken)
    {
        await dbContext.IdentifierTypesTranslations.AddRangeAsync(translationsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslations(List<IdentifierTypeTranslation> translationsToUpdate,
        CancellationToken cancellationToken)
    {
        dbContext.UpdateRange(translationsToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTranslations(List<IdentifierTypeTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        dbContext.RemoveRange(translationsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}