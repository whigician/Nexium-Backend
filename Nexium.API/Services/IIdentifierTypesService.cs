using Nexium.API.TransferObjects.IdentifierType;

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
}