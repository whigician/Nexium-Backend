using Nexium.API.TransferObjects.Currency;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services;

public interface ICurrenciesService
{
    public Task<List<CurrencyView>> GetAllCurrencies(CancellationToken cancellationToken);
    public Task<CurrencyView> GetSingleCurrencyById(byte currencyId, CancellationToken cancellationToken);

    public Task<CurrencyView> CreateCurrency(CurrencySave currencyToCreate,
        CancellationToken cancellationToken);

    public Task UpdateCurrency(byte currencyId, CurrencySave currencyToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteCurrency(byte currencyId, CancellationToken cancellationToken);

    Task<List<TranslationView>> GetASingleCurrencyTranslations(byte currencyId,
        CancellationToken cancellationToken);

    Task UpdateASingleCurrencyTranslations(byte currencyId,
        List<TranslationSave> currencyTranslationsToSave,
        CancellationToken cancellationToken);
}