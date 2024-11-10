using Nexium.API.TransferObjects.PersonIdentifierType;

namespace Nexium.API.Services;

public interface IPersonIdentifierTypesService
{
    public Task<List<PersonIdentifierTypeView>> GetAllPersonIdentifierTypes(CancellationToken cancellationToken);

    public Task<PersonIdentifierTypeView> GetSinglePersonIdentifierTypeById(byte personIdentifierTypeId,
        CancellationToken cancellationToken);

    public Task<PersonIdentifierTypeView> CreatePersonIdentifierType(
        PersonIdentifierTypeSave personIdentifierTypeToCreate,
        CancellationToken cancellationToken);

    public Task UpdatePersonIdentifierType(byte personIdentifierTypeId,
        PersonIdentifierTypeSave personIdentifierTypeToUpdate,
        CancellationToken cancellationToken);

    public Task DeletePersonIdentifierType(byte personIdentifierTypeId, CancellationToken cancellationToken);
}