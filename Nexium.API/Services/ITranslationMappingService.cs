using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services;

public interface ITranslationMappingService
{
    Task UpdateASingleAddressTypeTranslations(string entityTypeName,long entityId, string attributeName,
        List<TranslationMappingSave> addressTypeTranslationsToSave, CancellationToken cancellationToken);

    Task<List<TranslationMappingView>> GetASingleEntityTranslations(long entityId, string entityName,
        string attributeName, CancellationToken cancellationToken);
}