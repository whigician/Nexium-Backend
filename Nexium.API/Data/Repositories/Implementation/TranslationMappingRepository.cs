using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class TranslationMappingRepository(NexiumDbContext dbContext) : ITranslationMappingRepository
{
    public Task<TranslationMapping> GetSingleEntityTranslationForAttribute(string entityName, long entityId,
        string attributeName, string languageCode,
        CancellationToken cancellationToken)
    {
        return dbContext.TranslationMappings.SingleOrDefaultAsync(a =>
            a.EntityTypeName == entityName && a.AttributeName == attributeName && a.EntityId == entityId &&
            languageCode.Contains(a.LanguageCode), cancellationToken);
    }

    public Task<List<TranslationMapping>> GetASingleEntityTranslationsForAttribute(string entityName, long entityId,
        string attributeName,
        CancellationToken cancellationToken)
    {
        return dbContext.TranslationMappings.Where(a =>
                a.EntityTypeName == entityName && a.AttributeName == attributeName && a.EntityId == entityId)
            .ToListAsync(cancellationToken);
    }

    public async Task AddTranslations(List<TranslationMapping> translationsToCreate,
        CancellationToken cancellationToken)
    {
        await dbContext.TranslationMappings.AddRangeAsync(translationsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslations(List<TranslationMapping> translationsToUpdate,
        CancellationToken cancellationToken)
    {
        dbContext.TranslationMappings.UpdateRange(translationsToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTranslations(List<TranslationMapping> translationsToDelete,
        CancellationToken cancellationToken)
    {
        dbContext.TranslationMappings.RemoveRange(translationsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
    
}