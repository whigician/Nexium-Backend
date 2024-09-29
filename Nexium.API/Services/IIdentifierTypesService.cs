using Nexium.API.TransferObjects.IdentifierType;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services;

public interface IIdentifierTypesService
{
    public Task<List<IdentifierTypeView>> GetAllIdentifierTypes(CancellationToken cancellationToken);

    public Task<IdentifierTypeView> GetSingleIdentifierTypeById(byte identifierTypeId,
        CancellationToken cancellationToken);

    public Task<IdentifierTypeView> CreateIdentifierType(IdentifierTypeSave identifierTypeToCreate,
        CancellationToken cancellationToken);

    public Task UpdateIdentifierType(byte identifierTypeId, IdentifierTypeSave identifierTypeToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteIdentifierType(byte identifierTypeId, CancellationToken cancellationToken);

    Task<List<TranslationView>> GetASingleIdentifierTypeTranslations(byte identifierTypeId,
        CancellationToken cancellationToken);

    Task UpdateASingleIdentifierTypeTranslations(byte identifierTypeId,
        List<TranslationSave> identifierTypeTranslationsToSave,
        CancellationToken cancellationToken);
}