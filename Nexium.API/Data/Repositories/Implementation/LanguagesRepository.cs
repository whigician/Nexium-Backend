using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class LanguagesRepository(NexiumDbContext dbContext)
    : ILanguagesRepository
{
    public Task<List<Language>> GetAllLanguages(CancellationToken cancellationToken)
    {
        return dbContext.Languages.AsNoTracking().ToListAsync(cancellationToken);
    }

    public Task<Language> GetSingleLanguageByCode(string languageCode, CancellationToken cancellationToken)
    {
        return dbContext.Languages.FirstOrDefaultAsync(x => x.Code == languageCode, cancellationToken);
    }

    public async Task<Language> CreateLanguage(Language language, CancellationToken cancellationToken)
    {
        dbContext.Languages.Add(language);
        await dbContext.SaveChangesAsync(cancellationToken);
        return language;
    }

    public async Task UpdateLanguage(Language language, CancellationToken cancellationToken)
    {
        dbContext.Languages.Update(language);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteLanguage(Language existingLanguage, CancellationToken cancellationToken)
    {
        dbContext.Languages.Remove(existingLanguage);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}