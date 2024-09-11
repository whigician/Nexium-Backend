﻿using Microsoft.AspNetCore.Mvc;
using Nexium.API.Services;
using Nexium.API.TransferObjects.BusinessType;
using Nexium.API.TransferObjects.BusinessTypeTranslation;

namespace Nexium.API.Controllers;

[ApiController]
[Route("api/[Controller]")]
public class BusinessTypesController(IBusinessTypesService businessTypesService) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult> GetAllBusinessTypes(CancellationToken cancellationToken)
    {
        return Ok(await businessTypesService.GetAllBusinessTypes(cancellationToken));
    }

    [HttpGet("{businessTypeId:int}")]
    public async Task<ActionResult> GetSingleBusinessTypeById(byte businessTypeId, CancellationToken cancellationToken)
    {
        return Ok(await businessTypesService.GetSingleBusinessTypeById(businessTypeId, cancellationToken));
    }

    [HttpPost]
    public async Task<ActionResult> CreateBusinessType([FromBody] BusinessTypeSave businessTypeToCreate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        var result = await businessTypesService.CreateBusinessType(businessTypeToCreate, cancellationToken);
        return CreatedAtAction(nameof(GetSingleBusinessTypeById), new { businessTypeId = result.Id }, result);
    }

    [HttpPut("{businessTypeId:int}")]
    public async Task<ActionResult> UpdateBusinessType(byte businessTypeId, [FromBody] BusinessTypeSave businessTypeToUpdate,
        CancellationToken cancellationToken)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        await businessTypesService.UpdateBusinessType(businessTypeId, businessTypeToUpdate, cancellationToken);
        return NoContent();
    }

    [HttpDelete("{businessTypeId:int}")]
    public async Task<ActionResult> DeleteBusinessType(byte businessTypeId, CancellationToken cancellationToken)
    {
        await businessTypesService.DeleteBusinessType(businessTypeId, cancellationToken);
        return NoContent();
    }

    [HttpGet("translations/{businessTypeId:int}")]
    public async Task<ActionResult> GetASingleBusinessTypeTranslations(byte businessTypeId,
        CancellationToken cancellationToken)
    {
        return Ok(await businessTypesService.GetASingleBusinessTypeTranslations(businessTypeId, cancellationToken));
    }

    [HttpPut("translations/{businessTypeId:int}")]
    public async Task<ActionResult> UpdateASingleBusinessTypeTranslations(byte businessTypeId,
        [FromBody] List<BusinessTypeTranslationSave> businessTypeTranslationToSave, CancellationToken cancellationToken)
    {
        await businessTypesService.UpdateASingleBusinessTypeTranslations(businessTypeId, businessTypeTranslationToSave,
            cancellationToken);
        return NoContent();
    }
}