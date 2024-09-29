using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface ITargetMarketsRepository
{
    public Task<List<TargetMarket>>
        GetAllTargetMarkets(CancellationToken cancellationToken, string selectedLanguage = "fr-FR");

    public Task<TargetMarket> GetSingleTargetMarketById(byte targetMarketId, CancellationToken cancellationToken,
        string selectedLanguage = "fr-FR",
        bool forView = false);

    Task<TargetMarket> CreateTargetMarket(TargetMarket targetMarket, CancellationToken cancellationToken);
    Task UpdateTargetMarket(TargetMarket targetMarket, CancellationToken cancellationToken);
    Task DeleteTargetMarket(TargetMarket existingTargetMarket, CancellationToken cancellationToken);

    Task<List<TargetMarketTranslation>> GetASingleTargetMarketTranslations(byte targetMarketId,
        CancellationToken cancellationToken, bool forView = false);

    Task AddTranslations(List<TargetMarketTranslation> translationsToCreate, CancellationToken cancellationToken);
    Task UpdateTranslations(List<TargetMarketTranslation> translationsToUpdate, CancellationToken cancellationToken);
    Task RemoveTranslations(List<TargetMarketTranslation> translationsToDelete, CancellationToken cancellationToken);
}