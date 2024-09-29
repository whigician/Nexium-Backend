using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.Language;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class LanguagesController(ILanguagesService languagesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllLanguages(CancellationToken cancellationToken)
    {
        return Ok(await languagesService.GetAllLanguages(cancellationToken));
    }

    [HttpGet("{languageCode}")]
    public async Task<ActionResult> GetSingleLanguageByCode(string languageCode, CancellationToken cancellationToken)
    {
        return Ok(await languagesService.GetSingleLanguageByCode(languageCode, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateLanguage([FromBody] LanguageSave languageToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await languagesService.CreateLanguage(languageToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleLanguageByCode), new { languageCode = result.Code }, result);
    }

    [HttpPut("{languageCode}")]
    public async Task<ActionResult> UpdateLanguage([FromBody] LanguageSave languageToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await languagesService.UpdateLanguage(languageToUpdate, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{languageCode}")]
    public async Task<ActionResult> DeleteLanguage(string languageCode, CancellationToken cancellationToken)
    {
        await languagesService.DeleteLanguage(languageCode, cancellationToken);
        return NoContent();
    }
}