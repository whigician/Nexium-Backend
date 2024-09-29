using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.IdentifierType;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class IdentifierTypesController(IIdentifierTypesService identifierTypesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllIdentifierTypes(CancellationToken cancellationToken)
    {
        return Ok(await identifierTypesService.GetAllIdentifierTypes(cancellationToken));
    }

    [HttpGet("{identifierTypeId}")]
    public async Task<ActionResult> GetSingleIdentifierTypeById(byte identifierTypeId,
        CancellationToken cancellationToken)
    {
        return Ok(await identifierTypesService.GetSingleIdentifierTypeById(identifierTypeId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateIdentifierType([FromBody] IdentifierTypeSave identifierTypeToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await identifierTypesService.CreateIdentifierType(identifierTypeToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleIdentifierTypeById), new { identifierTypeId = result.Id }, result);
    }

    [HttpPut("{identifierTypeId}")]
    public async Task<ActionResult> UpdateIdentifierType(byte identifierTypeId,
        [FromBody] IdentifierTypeSave identifierTypeToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await identifierTypesService.UpdateIdentifierType(identifierTypeId, identifierTypeToUpdate, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{identifierTypeId}")]
    public async Task<ActionResult> DeleteIdentifierType(byte identifierTypeId, CancellationToken cancellationToken)
    {
        await identifierTypesService.DeleteIdentifierType(identifierTypeId, cancellationToken);
        return NoContent();
    }

    [HttpGet("translations/{identifierTypeId}")]
    public async Task<ActionResult> GetASingleIdentifierTypeTranslations(byte identifierTypeId,
        CancellationToken cancellationToken)
    {
        return Ok(
            await identifierTypesService.GetASingleIdentifierTypeTranslations(identifierTypeId, cancellationToken));
    }

    [HttpPut("translations/{identifierTypeId}")]
    public async Task<ActionResult> UpdateASingleIdentifierTypeTranslations(byte identifierTypeId,
        [FromBody] List<TranslationSave> translationsToSave, CancellationToken cancellationToken)
    {
        await identifierTypesService.UpdateASingleIdentifierTypeTranslations(identifierTypeId, translationsToSave,
            cancellationToken);
        return NoContent();
    }
}