using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IPersonIdentifierTypesRepository
{
    public Task<List<PersonIdentifierType>>
        GetAllPersonIdentifierTypes(CancellationToken cancellationToken);

    public Task<PersonIdentifierType> GetSinglePersonIdentifierTypeById(byte employeePositionId,
        CancellationToken cancellationToken,
        bool forView = false);

    Task<PersonIdentifierType> CreatePersonIdentifierType(PersonIdentifierType employeePosition,
        CancellationToken cancellationToken);

    Task UpdatePersonIdentifierType(PersonIdentifierType employeePosition, CancellationToken cancellationToken);

    Task DeletePersonIdentifierType(PersonIdentifierType existingPersonIdentifierType,
        CancellationToken cancellationToken);
}