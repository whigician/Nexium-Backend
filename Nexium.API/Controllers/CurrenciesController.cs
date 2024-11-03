using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.Currency;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class CurrenciesController(ICurrenciesService currenciesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllCurrencies(CancellationToken cancellationToken)
    {
        return Ok(await currenciesService.GetAllCurrencies(cancellationToken));
    }

    [HttpGet("{currencyId}")]
    public async Task<ActionResult> GetSingleCurrencyById(byte currencyId, CancellationToken cancellationToken)
    {
        return Ok(await currenciesService.GetSingleCurrencyById(currencyId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateCurrency([FromBody] CurrencySave currencyToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await currenciesService.CreateCurrency(currencyToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleCurrencyById), new { currencyId = result.Id }, result);
    }

    [HttpPut("{currencyId}")]
    public async Task<ActionResult> UpdateCurrency(byte currencyId,
        [FromBody] CurrencySave currencyToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await currenciesService.UpdateCurrency(currencyId, currencyToUpdate, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{currencyId}")]
    public async Task<ActionResult> DeleteCurrency(byte currencyId, CancellationToken cancellationToken)
    {
        await currenciesService.DeleteCurrency(currencyId, cancellationToken);
        return NoContent();
    }
}