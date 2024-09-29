using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.BusinessStatus;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BusinessStatusesController(IBusinessStatusesService businessStatusesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllBusinessStatuses(CancellationToken cancellationToken)
    {
        return Ok(await businessStatusesService.GetAllBusinessStatuses(cancellationToken));
    }

    [HttpGet("{businessStatusId}")]
    public async Task<ActionResult> GetSingleBusinessStatusById(byte businessStatusId,
        CancellationToken cancellationToken)
    {
        return Ok(await businessStatusesService.GetSingleBusinessStatusById(businessStatusId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateBusinessStatus([FromBody] BusinessStatusSave businessStatusToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await businessStatusesService.CreateBusinessStatus(businessStatusToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleBusinessStatusById), new { businessStatusId = result.Id }, result);
    }

    [HttpPut("{businessStatusId}")]
    public async Task<ActionResult> UpdateBusinessStatus(byte businessStatusId,
        [FromBody] BusinessStatusSave businessStatusToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await businessStatusesService.UpdateBusinessStatus(businessStatusId, businessStatusToUpdate, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{businessStatusId}")]
    public async Task<ActionResult> DeleteBusinessStatus(byte businessStatusId, CancellationToken cancellationToken)
    {
        await businessStatusesService.DeleteBusinessStatus(businessStatusId, cancellationToken);
        return NoContent();
    }

    [HttpGet("translations/{businessStatusId}")]
    public async Task<ActionResult> GetASingleBusinessStatusTranslations(byte businessStatusId,
        CancellationToken cancellationToken)
    {
        return Ok(await businessStatusesService.GetASingleBusinessStatusTranslations(businessStatusId,
            cancellationToken));
    }

    [HttpPut("translations/{businessStatusId}")]
    public async Task<ActionResult> UpdateASingleBusinessStatusTranslations(byte businessStatusId,
        [FromBody] List<TranslationSave> translationsToSave, CancellationToken cancellationToken)
    {
        await businessStatusesService.UpdateASingleBusinessStatusTranslations(businessStatusId, translationsToSave,
            cancellationToken);
        return NoContent();
    }
}