using Microsoft.EntityFrameworkCore;
using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories.Implementation;

public class EmployeePositionsRepository(NexiumDbContext dbContext)
    : IEmployeePositionsRepository
{
    public Task<List<EmployeePosition>> GetAllEmployeePositions(CancellationToken cancellationToken)
    {
        return dbContext.EmployeePositions
            .AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<EmployeePosition> GetSingleEmployeePositionById(ushort employeePositionId,
        CancellationToken cancellationToken, bool forView = false)
    {
        if (forView)
            return await dbContext.EmployeePositions.AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == employeePositionId, cancellationToken);
        return await dbContext.EmployeePositions.FirstOrDefaultAsync(x => x.Id == employeePositionId,
            cancellationToken);
    }

    public async Task<EmployeePosition> CreateEmployeePosition(EmployeePosition employeePosition,
        CancellationToken cancellationToken)
    {
        dbContext.EmployeePositions.Add(employeePosition);
        await dbContext.SaveChangesAsync(cancellationToken);
        return employeePosition;
    }

    public async Task UpdateEmployeePosition(EmployeePosition employeePosition, CancellationToken cancellationToken)
    {
        dbContext.EmployeePositions.Update(employeePosition);
        await dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteEmployeePosition(EmployeePosition existingEmployeePosition,
        CancellationToken cancellationToken)
    {
        dbContext.EmployeePositions.Remove(existingEmployeePosition);
        await dbContext.SaveChangesAsync(cancellationToken);
    }
}