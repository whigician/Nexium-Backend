using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.TargetMarket;

namespace Nexium.API.Services.Implementation;

public class TargetMarketsService(
    TargetMarketMapper mapper,
    ITargetMarketsRepository targetMarketsRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : ITargetMarketsService
{
    public async Task<List<TargetMarketView>> GetAllTargetMarkets(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var targetMarkets = await targetMarketsRepository.GetAllTargetMarkets(cancellationToken, selectedLanguage);

        var targetMarketViews = new List<TargetMarketView>();
        foreach (var x in targetMarkets)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "TargetMarket", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            targetMarketViews.Add(new TargetMarketView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return targetMarketViews;
    }

    public async Task<TargetMarketView> GetSingleTargetMarketById(byte targetMarketId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var targetMarket =
            await targetMarketsRepository.GetSingleTargetMarketById(targetMarketId, cancellationToken, selectedLanguage,
                true);
        if (targetMarket == null)
            throw new EntityNotFoundException(nameof(TargetMarket), nameof(targetMarketId), targetMarketId.ToString());
        return new TargetMarketView
        {
            Id = targetMarket.Id,
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("TargetMarket",
                targetMarket.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? targetMarket.Label
        };
    }

    public async Task<TargetMarketView> CreateTargetMarket(TargetMarketSave targetMarketToCreate,
        CancellationToken cancellationToken)
    {
        var targetMarket = mapper.MapToTargetMarket(targetMarketToCreate);
        var createdTargetMarket = await targetMarketsRepository.CreateTargetMarket(targetMarket, cancellationToken);
        return mapper.MapToTargetMarketView(createdTargetMarket);
    }

    public async Task UpdateTargetMarket(byte targetMarketId, TargetMarketSave targetMarketToUpdate,
        CancellationToken cancellationToken)
    {
        var targetMarketUpdatedValues = mapper.MapToTargetMarket(targetMarketToUpdate);
        var existingTargetMarket =
            await targetMarketsRepository.GetSingleTargetMarketById(targetMarketId, cancellationToken);
        if (existingTargetMarket == null)
            throw new EntityNotFoundException(nameof(TargetMarket), nameof(targetMarketId), targetMarketId.ToString());
        existingTargetMarket.Label = targetMarketUpdatedValues.Label;
        await targetMarketsRepository.UpdateTargetMarket(existingTargetMarket, cancellationToken);
    }

    public async Task DeleteTargetMarket(byte targetMarketId, CancellationToken cancellationToken)
    {
        var existingTargetMarket =
            await targetMarketsRepository.GetSingleTargetMarketById(targetMarketId, cancellationToken);
        if (existingTargetMarket == null)
            throw new EntityNotFoundException(nameof(TargetMarket), nameof(targetMarketId),
                targetMarketId.ToString());
        try
        {
            await targetMarketsRepository.DeleteTargetMarket(existingTargetMarket, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(TargetMarket), nameof(targetMarketId),
                targetMarketId.ToString());
        }
    }
}