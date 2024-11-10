using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class PersonIdentifierTypesRepository(NexiumDbContext dbContext)
    : IPersonIdentifierTypesRepository
{
    public Task<List<PersonIdentifierType>> GetAllPersonIdentifierTypes(CancellationToken cancellationToken)
    {
        return dbContext.PersonIdentifierTypes
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<PersonIdentifierType> GetSinglePersonIdentifierTypeById(byte personIdentifierTypeId,
        CancellationToken cancellationToken, bool forView = false)
    {
        if (forView)
            return await dbContext.PersonIdentifierTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == personIdentifierTypeId, cancellationToken);
        return await dbContext.PersonIdentifierTypes.FirstOrDefaultAsync(x => x.Id == personIdentifierTypeId,
            cancellationToken);
    }

    public async Task<PersonIdentifierType> CreatePersonIdentifierType(PersonIdentifierType personIdentifierType,
        CancellationToken cancellationToken)
    {
        dbContext.PersonIdentifierTypes.Add(personIdentifierType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return personIdentifierType;
    }

    public async Task UpdatePersonIdentifierType(PersonIdentifierType personIdentifierType,
        CancellationToken cancellationToken)
    {
        dbContext.PersonIdentifierTypes.Update(personIdentifierType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeletePersonIdentifierType(PersonIdentifierType existingPersonIdentifierType,
        CancellationToken cancellationToken)
    {
        dbContext.PersonIdentifierTypes.Remove(existingPersonIdentifierType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}