using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class BusinessLinkTypesRepository(NexiumDbContext dbContext)
    : IBusinessLinkTypesRepository
{
    public Task<List<BusinessLinkType>> GetAllBusinessLinkTypes(CancellationToken cancellationToken)
    {
        return dbContext.BusinessLinkTypes
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<BusinessLinkType> GetSingleBusinessLinkTypeById(byte businessLinkTypeId,
        CancellationToken cancellationToken,
        bool forView = false)
    {
        if (forView)
            return await dbContext.BusinessLinkTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == businessLinkTypeId, cancellationToken);
        return await dbContext.BusinessLinkTypes.FirstOrDefaultAsync(x => x.Id == businessLinkTypeId,
            cancellationToken);
    }

    public async Task<BusinessLinkType> CreateBusinessLinkType(BusinessLinkType businessLinkType,
        CancellationToken cancellationToken)
    {
        dbContext.BusinessLinkTypes.Add(businessLinkType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return businessLinkType;
    }

    public async Task UpdateBusinessLinkType(BusinessLinkType businessLinkType, CancellationToken cancellationToken)
    {
        dbContext.BusinessLinkTypes.Update(businessLinkType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBusinessLinkType(BusinessLinkType existingBusinessLinkType,
        CancellationToken cancellationToken)
    {
        dbContext.BusinessLinkTypes.Remove(existingBusinessLinkType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}