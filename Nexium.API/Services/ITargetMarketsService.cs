using Nexium.API.TransferObjects.TargetMarket;

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
}