using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.TargetMarket;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class TargetMarketsController(ITargetMarketsService targetMarketsService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllTargetMarkets(CancellationToken cancellationToken)
    {
        return Ok(await targetMarketsService.GetAllTargetMarkets(cancellationToken));
    }

    [HttpGet("{targetMarketId}")]
    public async Task<ActionResult> GetSingleTargetMarketById(byte targetMarketId, CancellationToken cancellationToken)
    {
        return Ok(await targetMarketsService.GetSingleTargetMarketById(targetMarketId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateTargetMarket([FromBody] TargetMarketSave targetMarketToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await targetMarketsService.CreateTargetMarket(targetMarketToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleTargetMarketById), new { targetMarketId = result.Id }, result);
    }

    [HttpPut("{targetMarketId}")]
    public async Task<ActionResult> UpdateTargetMarket(byte targetMarketId,
        [FromBody] TargetMarketSave targetMarketToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await targetMarketsService.UpdateTargetMarket(targetMarketId, targetMarketToUpdate, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{targetMarketId}")]
    public async Task<ActionResult> DeleteTargetMarket(byte targetMarketId, CancellationToken cancellationToken)
    {
        await targetMarketsService.DeleteTargetMarket(targetMarketId, cancellationToken);
        return NoContent();
    }

    [HttpGet("translations/{targetMarketId}")]
    public async Task<ActionResult> GetASingleTargetMarketTranslations(byte targetMarketId,
        CancellationToken cancellationToken)
    {
        return Ok(await targetMarketsService.GetASingleTargetMarketTranslations(targetMarketId, cancellationToken));
    }

    [HttpPut("translations/{targetMarketId}")]
    public async Task<ActionResult> UpdateASingleTargetMarketTranslations(byte targetMarketId,
        [FromBody] List<TranslationSave> translationsToSave, CancellationToken cancellationToken)
    {
        await targetMarketsService.UpdateASingleTargetMarketTranslations(targetMarketId, translationsToSave,
            cancellationToken);
        return NoContent();
    }
}