using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class IdentifierTypesRepository(NexiumDbContext dbContext)
    : IIdentifierTypesRepository
{
    public Task<List<IdentifierType>> GetAllIdentifierTypes(CancellationToken cancellationToken,
        string selectedLanguage)
    {
        return dbContext.IdentifierTypes
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IdentifierType> GetSingleIdentifierTypeById(byte identifierTypeId,
        CancellationToken cancellationToken,
        string selectedLanguage, bool forView = false)
    {
        if (forView)
            return await dbContext.IdentifierTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == identifierTypeId, cancellationToken);
        return await dbContext.IdentifierTypes.FirstOrDefaultAsync(x => x.Id == identifierTypeId, cancellationToken);
    }

    public async Task<IdentifierType> CreateIdentifierType(IdentifierType identifierType,
        CancellationToken cancellationToken)
    {
        dbContext.IdentifierTypes.Add(identifierType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return identifierType;
    }

    public async Task UpdateIdentifierType(IdentifierType identifierType, CancellationToken cancellationToken)
    {
        dbContext.IdentifierTypes.Update(identifierType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteIdentifierType(IdentifierType existingIdentifierType, CancellationToken cancellationToken)
    {
        dbContext.IdentifierTypes.Remove(existingIdentifierType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}