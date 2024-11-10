using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.PersonIdentifierType;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class PersonIdentifierTypesController(IPersonIdentifierTypesService personIdentifierTypesService)
    : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllPersonIdentifierTypes(CancellationToken cancellationToken)
    {
        return Ok(await personIdentifierTypesService.GetAllPersonIdentifierTypes(cancellationToken));
    }

    [HttpGet("{personIdentifierTypeId}")]
    public async Task<ActionResult> GetSinglePersonIdentifierTypeById(byte personIdentifierTypeId,
        CancellationToken cancellationToken)
    {
        return Ok(await personIdentifierTypesService.GetSinglePersonIdentifierTypeById(personIdentifierTypeId,
            cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreatePersonIdentifierType(
        [FromBody] PersonIdentifierTypeSave personIdentifierTypeToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result =
            await personIdentifierTypesService.CreatePersonIdentifierType(personIdentifierTypeToCreate,
                cancellationToken);
        return CreatedAtAction(nameof(GetSinglePersonIdentifierTypeById), new { personIdentifierTypeId = result.Id },
            result);
    }

    [HttpPut("{personIdentifierTypeId}")]
    public async Task<ActionResult> UpdatePersonIdentifierType(byte personIdentifierTypeId,
        [FromBody] PersonIdentifierTypeSave personIdentifierTypeToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await personIdentifierTypesService.UpdatePersonIdentifierType(personIdentifierTypeId,
            personIdentifierTypeToUpdate,
            cancellationToken);
        return NoContent();
    }

    [HttpDelete("{personIdentifierTypeId}")]
    public async Task<ActionResult> DeletePersonIdentifierType(byte personIdentifierTypeId,
        CancellationToken cancellationToken)
    {
        await personIdentifierTypesService.DeletePersonIdentifierType(personIdentifierTypeId, cancellationToken);
        return NoContent();
    }
}