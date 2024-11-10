using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface ITargetMarketsRepository
{
    public Task<List<TargetMarket>>
        GetAllTargetMarkets(CancellationToken cancellationToken);

    public Task<TargetMarket> GetSingleTargetMarketById(byte targetMarketId, CancellationToken cancellationToken,
        bool forView = false);

    Task<TargetMarket> CreateTargetMarket(TargetMarket targetMarket, CancellationToken cancellationToken);
    Task UpdateTargetMarket(TargetMarket targetMarket, CancellationToken cancellationToken);
    Task DeleteTargetMarket(TargetMarket existingTargetMarket, CancellationToken cancellationToken);
}