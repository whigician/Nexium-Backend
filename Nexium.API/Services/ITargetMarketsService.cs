using Nexium.API.TransferObjects.TargetMarket;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services;

public interface ITargetMarketsService
{
    public Task<List<TargetMarketView>> GetAllTargetMarkets(CancellationToken cancellationToken);
    public Task<TargetMarketView> GetSingleTargetMarketById(byte targetMarketId, CancellationToken cancellationToken);

    public Task<TargetMarketView> CreateTargetMarket(TargetMarketSave targetMarketToCreate,
        CancellationToken cancellationToken);

    public Task UpdateTargetMarket(byte targetMarketId, TargetMarketSave targetMarketToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteTargetMarket(byte targetMarketId, CancellationToken cancellationToken);

    Task<List<TranslationView>> GetASingleTargetMarketTranslations(byte targetMarketId,
        CancellationToken cancellationToken);

    Task UpdateASingleTargetMarketTranslations(byte targetMarketId,
        List<TranslationSave> targetMarketTranslationsToSave,
        CancellationToken cancellationToken);
}