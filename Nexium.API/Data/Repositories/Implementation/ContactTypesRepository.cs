using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class ContactTypesRepository(NexiumDbContext dbContext)
    : IContactTypesRepository
{
    public Task<List<ContactType>> GetAllContactTypes(CancellationToken cancellationToken,
        string selectedLanguage)
    {
        return dbContext.ContactTypes
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<ContactType> GetSingleContactTypeById(byte contactTypeId, CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        if (forView)
            return await dbContext.ContactTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == contactTypeId, cancellationToken);
        return await dbContext.ContactTypes.FirstOrDefaultAsync(x => x.Id == contactTypeId, cancellationToken);
    }

    public async Task<ContactType> CreateContactType(ContactType contactType, CancellationToken cancellationToken)
    {
        dbContext.ContactTypes.Add(contactType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return contactType;
    }

    public async Task UpdateContactType(ContactType contactType, CancellationToken cancellationToken)
    {
        dbContext.ContactTypes.Update(contactType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteContactType(ContactType existingContactType, CancellationToken cancellationToken)
    {
        dbContext.ContactTypes.Remove(existingContactType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}