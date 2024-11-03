using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface ITargetMarketsRepository
{
    public Task<List<TargetMarket>>
        GetAllTargetMarkets(CancellationToken cancellationToken, string selectedLanguage);

    public Task<TargetMarket> GetSingleTargetMarketById(byte targetMarketId, CancellationToken cancellationToken,
        string selectedLanguage = "fr",
        bool forView = false);

    Task<TargetMarket> CreateTargetMarket(TargetMarket targetMarket, CancellationToken cancellationToken);
    Task UpdateTargetMarket(TargetMarket targetMarket, CancellationToken cancellationToken);
    Task DeleteTargetMarket(TargetMarket existingTargetMarket, CancellationToken cancellationToken);
}