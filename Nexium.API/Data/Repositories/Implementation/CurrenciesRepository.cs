using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class CurrenciesRepository(NexiumDbContext dbContext)
    : ICurrenciesRepository
{
    public Task<List<Currency>> GetAllCurrencies(CancellationToken cancellationToken,
        string selectedLanguage)
    {
        return dbContext.Currencies
            .Include(i => i.Translations.Where(t => t.LanguageCode == selectedLanguage))
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Currency> GetSingleCurrencyById(byte currencyId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        var fetchQuery =
            dbContext.Currencies.Include(x => x.Translations.Where(t => t.LanguageCode == selectedLanguage));
        if (forView)
            return await fetchQuery.AsNoTracking().FirstOrDefaultAsync(x => x.Id == currencyId, cancellationToken);
        return await fetchQuery.FirstOrDefaultAsync(x => x.Id == currencyId, cancellationToken);
    }

    public async Task<Currency> CreateCurrency(Currency currency, CancellationToken cancellationToken)
    {
        dbContext.Currencies.Add(currency);
        await dbContext.SaveChangesAsync(cancellationToken);
        return currency;
    }

    public async Task UpdateCurrency(Currency currency, CancellationToken cancellationToken)
    {
        dbContext.Currencies.Update(currency);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteCurrency(Currency existingCurrency, CancellationToken cancellationToken)
    {
        dbContext.Currencies.Remove(existingCurrency);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public Task<List<CurrencyTranslation>> GetASingleCurrencyTranslations(byte currencyId,
        CancellationToken cancellationToken, bool forView = false)
    {
        var fetchQuery = dbContext.CurrenciesTranslations
            .Where(x => x.CurrencyId == currencyId);
        return forView
            ? fetchQuery.AsNoTracking().ToListAsync(cancellationToken)
            : fetchQuery.ToListAsync(cancellationToken);
    }

    public async Task AddTranslations(List<CurrencyTranslation> translationsToCreate,
        CancellationToken cancellationToken)
    {
        await dbContext.CurrenciesTranslations.AddRangeAsync(translationsToCreate, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task UpdateTranslations(List<CurrencyTranslation> translationsToUpdate,
        CancellationToken cancellationToken)
    {
        dbContext.UpdateRange(translationsToUpdate);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task RemoveTranslations(List<CurrencyTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        dbContext.RemoveRange(translationsToDelete);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}