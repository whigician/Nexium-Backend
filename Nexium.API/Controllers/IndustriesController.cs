using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.Industry;
using Nexium.API.TransferObjects.IndustryTranslation;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class IndustriesController(IIndustriesService industriesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllIndustries(CancellationToken cancellationToken)
    {
        return Ok(await industriesService.GetAllIndustries(cancellationToken));
    }

    [HttpGet("{industryId:int}")]
    public async Task<ActionResult> GetSingleIndustryById(short industryId, CancellationToken cancellationToken)
    {
        return Ok(await industriesService.GetSingleIndustryById(industryId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateIndustry([FromBody] IndustrySave industryToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await industriesService.CreateIndustry(industryToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleIndustryById), new { industryId = result.Id }, result);
    }

    [HttpPut("{industryId:int}")]
    public async Task<ActionResult> UpdateIndustry(short industryId, [FromBody] IndustrySave industryToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await industriesService.UpdateIndustry(industryId, industryToUpdate, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{industryId:int}")]
    public async Task<ActionResult> DeleteIndustry(short industryId, CancellationToken cancellationToken)
    {
        await industriesService.DeleteIndustry(industryId, cancellationToken);
        return NoContent();
    }

    [HttpGet("translations/{industryId:int}")]
    public async Task<ActionResult> GetASingleIndustryTranslations(short industryId,
        CancellationToken cancellationToken)
    {
        return Ok(await industriesService.GetASingleIndustryTranslations(industryId, cancellationToken));
    }

    [HttpPut("translations/{industryId:int}")]
    public async Task<ActionResult> UpdateASingleIndustryTranslations(short industryId,
        [FromBody] List<IndustryTranslationSave> industryTranslationToSave, CancellationToken cancellationToken)
    {
        await industriesService.UpdateASingleIndustryTranslations(industryId, industryTranslationToSave,
            cancellationToken);
        return NoContent();
    }
}