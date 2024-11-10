using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface ICurrenciesRepository
{
    public Task<List<Currency>>
        GetAllCurrencies(CancellationToken cancellationToken);

    public Task<Currency> GetSingleCurrencyById(byte currencyId, CancellationToken cancellationToken,
        bool forView = false);

    Task<Currency> CreateCurrency(Currency currency, CancellationToken cancellationToken);
    Task UpdateCurrency(Currency currency, CancellationToken cancellationToken);
    Task DeleteCurrency(Currency existingCurrency, CancellationToken cancellationToken);
}