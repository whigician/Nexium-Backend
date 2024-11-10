using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface IEmployeePositionsRepository
{
    public Task<List<EmployeePosition>>
        GetAllEmployeePositions(CancellationToken cancellationToken);

    public Task<EmployeePosition> GetSingleEmployeePositionById(ushort employeePositionId,
        CancellationToken cancellationToken,
        bool forView = false);

    Task<EmployeePosition> CreateEmployeePosition(EmployeePosition employeePosition,
        CancellationToken cancellationToken);

    Task UpdateEmployeePosition(EmployeePosition employeePosition, CancellationToken cancellationToken);
    Task DeleteEmployeePosition(EmployeePosition existingEmployeePosition, CancellationToken cancellationToken);
}