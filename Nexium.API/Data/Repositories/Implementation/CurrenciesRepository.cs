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
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<Currency> GetSingleCurrencyById(byte currencyId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        if (forView)
            return await dbContext.Currencies.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == currencyId, cancellationToken);
        return await dbContext.Currencies.FirstOrDefaultAsync(x => x.Id == currencyId, cancellationToken);
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
}