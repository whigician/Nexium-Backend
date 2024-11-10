using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class TranslationMappingsController(ITranslationMappingService translationMappingService) : ControllerBase
{
    [HttpGet("/{entityId}")]
    public async Task<ActionResult> GetASingleEntityTranslations(long entityId, string entityName, string attributeName,
        CancellationToken cancellationToken)
    {
        return Ok(await translationMappingService.GetASingleEntityTranslations(entityId, entityName, attributeName,
            cancellationToken));
    }

    [HttpPut("{entityName}/{entityId}")]
    public async Task<ActionResult> UpdateASingleAddressTypeTranslations(string entityName, long entityId,
        string attributeName,
        [FromBody] List<TranslationMappingSave> translationsToSave, CancellationToken cancellationToken)
    {
        await translationMappingService.UpdateASingleAddressTypeTranslations(entityName, entityId, attributeName,
            translationsToSave,
            cancellationToken);
        return NoContent();
    }
}