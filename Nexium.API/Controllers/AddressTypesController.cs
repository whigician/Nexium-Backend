using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.AddressType;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class AddressTypesController(IAddressTypesService addressTypesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllAddressTypes(CancellationToken cancellationToken)
    {
        return Ok(await addressTypesService.GetAllAddressTypes(cancellationToken));
    }

    [HttpGet("{addressTypeId}")]
    public async Task<ActionResult> GetSingleAddressTypeById(byte addressTypeId, CancellationToken cancellationToken)
    {
        return Ok(await addressTypesService.GetSingleAddressTypeById(addressTypeId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateAddressType([FromBody] AddressTypeSave addressTypeToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await addressTypesService.CreateAddressType(addressTypeToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleAddressTypeById), new { addressTypeId = result.Id }, result);
    }

    [HttpPut("{addressTypeId}")]
    public async Task<ActionResult> UpdateAddressType(byte addressTypeId,
        [FromBody] AddressTypeSave addressTypeToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await addressTypesService.UpdateAddressType(addressTypeId, addressTypeToUpdate, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{addressTypeId}")]
    public async Task<ActionResult> DeleteAddressType(byte addressTypeId, CancellationToken cancellationToken)
    {
        await addressTypesService.DeleteAddressType(addressTypeId, cancellationToken);
        return NoContent();
    }
}