using Nexium.API.Entities;
using Nexium.API.TransferObjects.EmployeePosition;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class EmployeePositionMapper
{
    public partial EmployeePosition MapToEmployeePosition(EmployeePositionSave employeePositionSave);
    public partial EmployeePositionView MapToEmployeePositionView(EmployeePosition employeePosition);
}