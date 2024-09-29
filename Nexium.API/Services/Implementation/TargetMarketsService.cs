using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.TargetMarket;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services.Implementation;

public class TargetMarketsService(
    TargetMarketMapper mapper,
    ITargetMarketsRepository targetMarketsRepository,
    SelectedLanguageService selectedLanguageService,
    TranslationMapper translationMapper) : ITargetMarketsService
{
    public async Task<List<TargetMarketView>> GetAllTargetMarkets(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var targetMarkets = await targetMarketsRepository.GetAllTargetMarkets(cancellationToken, selectedLanguage);
        return targetMarkets.Select(x => new TargetMarketView
        {
            Id = x.Id,
            Label = x.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ?? x.Label
        }).ToList();
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
            Label =
                targetMarket.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ??
                targetMarket.Label
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

    public async Task<List<TranslationView>> GetASingleTargetMarketTranslations(byte targetMarketId,
        CancellationToken cancellationToken)
    {
        return translationMapper.MapToTargetMarketTranslationViewList(
            await targetMarketsRepository.GetASingleTargetMarketTranslations(targetMarketId, cancellationToken, true));
    }

    public async Task UpdateASingleTargetMarketTranslations(byte targetMarketId,
        List<TranslationSave> targetMarketTranslationsToSave, CancellationToken cancellationToken)
    {
        var existingTranslations =
            await targetMarketsRepository.GetASingleTargetMarketTranslations(targetMarketId, cancellationToken);
        var translationsToCreate =
            translationMapper.MapToTargetMarketTranslationList(targetMarketTranslationsToSave
                .Where(x => x.Id is 0 or null).ToList());
        translationsToCreate.ForEach(t => t.TargetMarketId = targetMarketId);
        var translationsUpdateInformation = targetMarketTranslationsToSave
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
            .Where(et => targetMarketTranslationsToSave.All(nt => nt.Id != et.Id && nt.Id != null && nt.Id != 0))
            .ToList();

        await SaveTranslations(translationsToCreate, translationsToUpdate, translationsToDelete, cancellationToken);
    }

    private async Task SaveTranslations(List<TargetMarketTranslation> translationsToCreate,
        List<TargetMarketTranslation> translationsToUpdate, List<TargetMarketTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        try
        {
            if (translationsToCreate.Count != 0)
                await targetMarketsRepository.AddTranslations(translationsToCreate, cancellationToken);

            if (translationsToUpdate.Count != 0)
                await targetMarketsRepository.UpdateTranslations(translationsToUpdate, cancellationToken);
        }
        catch (UniqueConstraintException ex)
        {
            throw new LanguageCodeAlreadyExists("", nameof(TargetMarket));
        }

        if (translationsToDelete.Count != 0)
            await targetMarketsRepository.RemoveTranslations(translationsToDelete, cancellationToken);
    }
}