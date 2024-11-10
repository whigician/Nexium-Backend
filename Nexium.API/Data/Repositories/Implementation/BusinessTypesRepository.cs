using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class BusinessTypesRepository(NexiumDbContext dbContext)
    : IBusinessTypesRepository
{
    public Task<List<BusinessType>> GetAllBusinessTypes(CancellationToken cancellationToken)
    {
        return dbContext.BusinessTypes
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<BusinessType> GetSingleBusinessTypeById(byte businessTypeId, CancellationToken cancellationToken,
        bool forView = false)
    {
        if (forView)
            return await dbContext.BusinessTypes.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == businessTypeId, cancellationToken);
        return await dbContext.BusinessTypes.FirstOrDefaultAsync(x => x.Id == businessTypeId, cancellationToken);
    }

    public async Task<BusinessType> CreateBusinessType(BusinessType businessType, CancellationToken cancellationToken)
    {
        dbContext.BusinessTypes.Add(businessType);
        await dbContext.SaveChangesAsync(cancellationToken);
        return businessType;
    }

    public async Task UpdateBusinessType(BusinessType businessType, CancellationToken cancellationToken)
    {
        dbContext.BusinessTypes.Update(businessType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBusinessType(BusinessType existingBusinessType, CancellationToken cancellationToken)
    {
        dbContext.BusinessTypes.Remove(existingBusinessType);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}