using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class BusinessStatusesRepository(NexiumDbContext dbContext)
    : IBusinessStatusesRepository
{
    public Task<List<BusinessStatus>> GetAllBusinessStatuses(CancellationToken cancellationToken)
    {
        return dbContext.BusinessStatuses
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<BusinessStatus> GetSingleBusinessStatusById(byte businessStatusId,
        CancellationToken cancellationToken, bool forView = false)
    {
        if (forView)
            return await dbContext.BusinessStatuses.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == businessStatusId, cancellationToken);
        return await dbContext.BusinessStatuses.FirstOrDefaultAsync(x => x.Id == businessStatusId, cancellationToken);
    }

    public async Task<BusinessStatus> CreateBusinessStatus(BusinessStatus businessStatus,
        CancellationToken cancellationToken)
    {
        dbContext.BusinessStatuses.Add(businessStatus);
        await dbContext.SaveChangesAsync(cancellationToken);
        return businessStatus;
    }

    public async Task UpdateBusinessStatus(BusinessStatus businessStatus, CancellationToken cancellationToken)
    {
        dbContext.BusinessStatuses.Update(businessStatus);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteBusinessStatus(BusinessStatus existingBusinessStatus, CancellationToken cancellationToken)
    {
        dbContext.BusinessStatuses.Remove(existingBusinessStatus);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}