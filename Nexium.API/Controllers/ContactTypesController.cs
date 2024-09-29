using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.ContactType;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class ContactTypesController(IContactTypesService contactTypesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllContactTypes(CancellationToken cancellationToken)
    {
        return Ok(await contactTypesService.GetAllContactTypes(cancellationToken));
    }

    [HttpGet("{contactTypeId}")]
    public async Task<ActionResult> GetSingleContactTypeById(byte contactTypeId, CancellationToken cancellationToken)
    {
        return Ok(await contactTypesService.GetSingleContactTypeById(contactTypeId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateContactType([FromBody] ContactTypeSave contactTypeToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await contactTypesService.CreateContactType(contactTypeToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleContactTypeById), new { contactTypeId = result.Id }, result);
    }

    [HttpPut("{contactTypeId}")]
    public async Task<ActionResult> UpdateContactType(byte contactTypeId,
        [FromBody] ContactTypeSave contactTypeToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await contactTypesService.UpdateContactType(contactTypeId, contactTypeToUpdate, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{contactTypeId}")]
    public async Task<ActionResult> DeleteContactType(byte contactTypeId, CancellationToken cancellationToken)
    {
        await contactTypesService.DeleteContactType(contactTypeId, cancellationToken);
        return NoContent();
    }

    [HttpGet("translations/{contactTypeId}")]
    public async Task<ActionResult> GetASingleContactTypeTranslations(byte contactTypeId,
        CancellationToken cancellationToken)
    {
        return Ok(await contactTypesService.GetASingleContactTypeTranslations(contactTypeId, cancellationToken));
    }

    [HttpPut("translations/{contactTypeId}")]
    public async Task<ActionResult> UpdateASingleContactTypeTranslations(byte contactTypeId,
        [FromBody] List<TranslationSave> translationsToSave, CancellationToken cancellationToken)
    {
        await contactTypesService.UpdateASingleContactTypeTranslations(contactTypeId, translationsToSave,
            cancellationToken);
        return NoContent();
    }
}