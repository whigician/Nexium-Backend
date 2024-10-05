using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface ICurrenciesRepository
{
    public Task<List<Currency>>
        GetAllCurrencies(CancellationToken cancellationToken, string selectedLanguage = "fr-FR");

    public Task<Currency> GetSingleCurrencyById(byte currencyId, CancellationToken cancellationToken,
        string selectedLanguage = "fr",
        bool forView = false);

    Task<Currency> CreateCurrency(Currency currency, CancellationToken cancellationToken);
    Task UpdateCurrency(Currency currency, CancellationToken cancellationToken);
    Task DeleteCurrency(Currency existingCurrency, CancellationToken cancellationToken);

    Task<List<CurrencyTranslation>> GetASingleCurrencyTranslations(byte currencyId,
        CancellationToken cancellationToken, bool forView = false);

    Task AddTranslations(List<CurrencyTranslation> translationsToCreate, CancellationToken cancellationToken);
    Task UpdateTranslations(List<CurrencyTranslation> translationsToUpdate, CancellationToken cancellationToken);
    Task RemoveTranslations(List<CurrencyTranslation> translationsToDelete, CancellationToken cancellationToken);
}