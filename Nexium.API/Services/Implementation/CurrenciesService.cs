using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.Currency;

namespace Nexium.API.Services.Implementation;

public class CurrenciesService(
    CurrencyMapper mapper,
    ICurrenciesRepository currenciesRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : ICurrenciesService
{
    public async Task<List<CurrencyView>> GetAllCurrencies(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessTypes = await currenciesRepository.GetAllCurrencies(cancellationToken, selectedLanguage);

        var currencyViews = new List<CurrencyView>();
        foreach (var x in businessTypes)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "Currency", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            currencyViews.Add(new CurrencyView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return currencyViews;
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
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("Currency", currency.Id,
                "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? currency.Label
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
}