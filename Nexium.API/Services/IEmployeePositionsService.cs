using Nexium.API.TransferObjects.EmployeePosition;

namespace Nexium.API.Services;

public interface IEmployeePositionsService
{
    public Task<List<EmployeePositionView>> GetAllEmployeePositions(CancellationToken cancellationToken);

    public Task<EmployeePositionView> GetSingleEmployeePositionById(ushort employeePositionId,
        CancellationToken cancellationToken);

    public Task<EmployeePositionView> CreateEmployeePosition(EmployeePositionSave employeePositionToCreate,
        CancellationToken cancellationToken);

    public Task UpdateEmployeePosition(ushort employeePositionId, EmployeePositionSave employeePositionToUpdate,
        CancellationToken cancellationToken);

    public Task DeleteEmployeePosition(ushort employeePositionId, CancellationToken cancellationToken);
}