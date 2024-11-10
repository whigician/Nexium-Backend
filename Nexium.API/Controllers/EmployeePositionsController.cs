using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.EmployeePosition;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class EmployeePositionsController(IEmployeePositionsService employeePositionsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllEmployeePositions(CancellationToken cancellationToken)
    {
        return Ok(await employeePositionsService.GetAllEmployeePositions(cancellationToken));
    }

    [HttpGet("{employeePositionId}")]
    public async Task<ActionResult> GetSingleEmployeePositionById(ushort employeePositionId,
        CancellationToken cancellationToken)
    {
        return Ok(await employeePositionsService.GetSingleEmployeePositionById(employeePositionId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateEmployeePosition([FromBody] EmployeePositionSave employeePositionToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await employeePositionsService.CreateEmployeePosition(employeePositionToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleEmployeePositionById), new { employeePositionId = result.Id }, result);
    }

    [HttpPut("{employeePositionId}")]
    public async Task<ActionResult> UpdateEmployeePosition(ushort employeePositionId,
        [FromBody] EmployeePositionSave employeePositionToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await employeePositionsService.UpdateEmployeePosition(employeePositionId, employeePositionToUpdate,
            cancellationToken);
        return NoContent();
    }

    [HttpDelete("{employeePositionId}")]
    public async Task<ActionResult> DeleteEmployeePosition(ushort employeePositionId,
        CancellationToken cancellationToken)
    {
        await employeePositionsService.DeleteEmployeePosition(employeePositionId, cancellationToken);
        return NoContent();
    }
}