using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IContactTypesRepository
{
    public Task<List<ContactType>>
        GetAllContactTypes(CancellationToken cancellationToken);

    public Task<ContactType> GetSingleContactTypeById(byte contactTypeId, CancellationToken cancellationToken,
        bool forView = false);

    Task<ContactType> CreateContactType(ContactType contactType, CancellationToken cancellationToken);
    Task UpdateContactType(ContactType contactType, CancellationToken cancellationToken);
    Task DeleteContactType(ContactType existingContactType, CancellationToken cancellationToken);
}