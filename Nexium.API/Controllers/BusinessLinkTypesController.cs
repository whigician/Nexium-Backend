using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects;
using Nexium.API.TransferObjects.BusinessLinkType;
using Nexium.API.Validators;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BusinessLinkTypesController(IBusinessLinkTypesService businessLinkTypesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllBusinessLinkTypes(CancellationToken cancellationToken)
    {
        return Ok(await businessLinkTypesService.GetAllBusinessLinkTypes(cancellationToken));
    }

    [HttpGet("{businessLinkTypeId}")]
    public async Task<ActionResult> GetSingleBusinessLinkTypeById(byte businessLinkTypeId,
        CancellationToken cancellationToken)
    {
        return Ok(await businessLinkTypesService.GetSingleBusinessLinkTypeById(businessLinkTypeId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateBusinessLinkType([FromBody] BusinessLinkTypeSave businessLinkTypeToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await businessLinkTypesService.CreateBusinessLinkType(businessLinkTypeToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleBusinessLinkTypeById), new { businessLinkTypeId = result.Id }, result);
    }

    [HttpPut("{businessLinkTypeId}")]
    public async Task<ActionResult> UpdateBusinessLinkType(byte businessLinkTypeId,
        [FromBody] BusinessLinkTypeSave businessLinkTypeToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await businessLinkTypesService.UpdateBusinessLinkType(businessLinkTypeId, businessLinkTypeToUpdate,
            cancellationToken);
        return NoContent();
    }

    [HttpDelete("{businessLinkTypeId}")]
    public async Task<ActionResult> DeleteBusinessLinkType(byte businessLinkTypeId, CancellationToken cancellationToken)
    {
        await businessLinkTypesService.DeleteBusinessLinkType(businessLinkTypeId, cancellationToken);
        return NoContent();
    }

    [HttpPost("logo/{businessLinkTypeId}")]
    public async Task<ActionResult<UploadResponse>> UploadEnterpriseLogo(byte businessLinkTypeId,
        [FileValidation(5 * 1024 * 1024, ".svg", ".jpg", ".jpeg", ".png", ".gif")] IFormFile logo)
    {
        if (!ModelState.IsValid) return BadRequest();
        return Ok(await businessLinkTypesService.UploadLogo(businessLinkTypeId, logo));
    }
}