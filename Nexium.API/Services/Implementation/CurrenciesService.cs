using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.Currency;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services.Implementation;

public class CurrenciesService(
    CurrencyMapper mapper,
    ICurrenciesRepository currenciesRepository,
    SelectedLanguageService selectedLanguageService,
    TranslationMapper translationMapper) : ICurrenciesService
{
    public async Task<List<CurrencyView>> GetAllCurrencies(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var currencies = await currenciesRepository.GetAllCurrencies(cancellationToken, selectedLanguage);
        return currencies.Select(x => new CurrencyView
        {
            Id = x.Id,
            Label = x.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ?? x.Label
        }).ToList();
    }

    public async Task<CurrencyView> GetSingleCurrencyById(byte currencyId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var currency =
            await currenciesRepository.GetSingleCurrencyById(currencyId, cancellationToken, selectedLanguage,
                true);
        if (currency == null)
            throw new EntityNotFoundException(nameof(Currency), nameof(currencyId), currencyId.ToString());
        return new CurrencyView
        {
            Id = currency.Id,
            Label =
                currency.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ??
                currency.Label
        };
    }

    public async Task<CurrencyView> CreateCurrency(CurrencySave currencyToCreate,
        CancellationToken cancellationToken)
    {
        var currency = mapper.MapToCurrency(currencyToCreate);
        var createdCurrency = await currenciesRepository.CreateCurrency(currency, cancellationToken);
        return mapper.MapToCurrencyView(createdCurrency);
    }

    public async Task UpdateCurrency(byte currencyId, CurrencySave currencyToUpdate,
        CancellationToken cancellationToken)
    {
        var currencyUpdatedValues = mapper.MapToCurrency(currencyToUpdate);
        var existingCurrency =
            await currenciesRepository.GetSingleCurrencyById(currencyId, cancellationToken);
        if (existingCurrency == null)
            throw new EntityNotFoundException(nameof(Currency), nameof(currencyId), currencyId.ToString());
        existingCurrency.Label = currencyUpdatedValues.Label;
        await currenciesRepository.UpdateCurrency(existingCurrency, cancellationToken);
    }

    public async Task DeleteCurrency(byte currencyId, CancellationToken cancellationToken)
    {
        var existingCurrency =
            await currenciesRepository.GetSingleCurrencyById(currencyId, cancellationToken);
        if (existingCurrency == null)
            throw new EntityNotFoundException(nameof(Currency), nameof(currencyId),
                currencyId.ToString());
        try
        {
            await currenciesRepository.DeleteCurrency(existingCurrency, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(Currency), nameof(currencyId),
                currencyId.ToString());
        }
    }

    public async Task<List<TranslationView>> GetASingleCurrencyTranslations(byte currencyId,
        CancellationToken cancellationToken)
    {
        return translationMapper.MapToCurrencyTranslationViewList(
            await currenciesRepository.GetASingleCurrencyTranslations(currencyId, cancellationToken, true));
    }

    public async Task UpdateASingleCurrencyTranslations(byte currencyId,
        List<TranslationSave> currencyTranslationsToSave, CancellationToken cancellationToken)
    {
        var existingTranslations =
            await currenciesRepository.GetASingleCurrencyTranslations(currencyId, cancellationToken);
        var translationsToCreate =
            translationMapper.MapToCurrencyTranslationList(currencyTranslationsToSave
                .Where(x => x.Id is 0 or null).ToList());
        translationsToCreate.ForEach(t => t.CurrencyId = currencyId);
        var translationsUpdateInformation = currencyTranslationsToSave
            .Where(x => existingTranslations.Any(i => i.Id == x.Id)).ToList();
        var translationsToUpdate = translationsUpdateInformation.Select(x =>
        {
            var translationToBeUpdated = existingTranslations.First(et => et.Id == x.Id);
            if (translationToBeUpdated.LanguageCode == x.LanguageCode &&
                translationToBeUpdated.TranslatedLabel == x.TranslatedLabel) return null;
            translationToBeUpdated.LanguageCode = x.LanguageCode;
            translationToBeUpdated.TranslatedLabel = x.TranslatedLabel;
            return translationToBeUpdated;
        }).Where(x => x != null).ToList();
        var translationsToDelete = existingTranslations
            .Where(et => currencyTranslationsToSave.All(nt => nt.Id != et.Id && nt.Id != null && nt.Id != 0))
            .ToList();

        await SaveTranslations(translationsToCreate, translationsToUpdate, translationsToDelete, cancellationToken);
    }

    private async Task SaveTranslations(List<CurrencyTranslation> translationsToCreate,
        List<CurrencyTranslation> translationsToUpdate, List<CurrencyTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        try
        {
            if (translationsToCreate.Count != 0)
                await currenciesRepository.AddTranslations(translationsToCreate, cancellationToken);

            if (translationsToUpdate.Count != 0)
                await currenciesRepository.UpdateTranslations(translationsToUpdate, cancellationToken);
        }
        catch (UniqueConstraintException)
        {
            throw new LanguageCodeAlreadyExists("", nameof(Currency));
        }

        if (translationsToDelete.Count != 0)
            await currenciesRepository.RemoveTranslations(translationsToDelete, cancellationToken);
    }
}