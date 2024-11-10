using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.BusinessType;

namespace Nexium.API.Services.Implementation;

public class BusinessTypesService(
    BusinessTypeMapper mapper,
    IBusinessTypesRepository businessTypesRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : IBusinessTypesService
{
    public async Task<List<BusinessTypeView>> GetAllBusinessTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessTypes = await businessTypesRepository.GetAllBusinessTypes(cancellationToken);

        var businessStatusViews = new List<BusinessTypeView>();
        foreach (var x in businessTypes)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "BusinessType", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            businessStatusViews.Add(new BusinessTypeView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return businessStatusViews;
    }

    public async Task<BusinessTypeView> GetSingleBusinessTypeById(byte businessTypeId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessType =
            await businessTypesRepository.GetSingleBusinessTypeById(businessTypeId, cancellationToken,
                true);
        if (businessType == null)
            throw new EntityNotFoundException(nameof(BusinessType), nameof(businessTypeId), businessTypeId.ToString());
        return new BusinessTypeView
        {
            Id = businessType.Id,
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("BusinessType",
                businessType.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? businessType.Label
        };
    }

    public async Task<BusinessTypeView> CreateBusinessType(BusinessTypeSave businessTypeToCreate,
        CancellationToken cancellationToken)
    {
        var businessType = mapper.MapToBusinessType(businessTypeToCreate);
        var createdBusinessType = await businessTypesRepository.CreateBusinessType(businessType, cancellationToken);
        return mapper.MapToBusinessTypeView(createdBusinessType);
    }

    public async Task UpdateBusinessType(byte businessTypeId, BusinessTypeSave businessTypeToUpdate,
        CancellationToken cancellationToken)
    {
        var businessTypeUpdatedValues = mapper.MapToBusinessType(businessTypeToUpdate);
        var existingBusinessType =
            await businessTypesRepository.GetSingleBusinessTypeById(businessTypeId, cancellationToken);
        if (existingBusinessType == null)
            throw new EntityNotFoundException(nameof(BusinessType), nameof(businessTypeId), businessTypeId.ToString());
        existingBusinessType.Label = businessTypeUpdatedValues.Label;
        await businessTypesRepository.UpdateBusinessType(existingBusinessType, cancellationToken);
    }

    public async Task DeleteBusinessType(byte businessTypeId, CancellationToken cancellationToken)
    {
        var existingBusinessType =
            await businessTypesRepository.GetSingleBusinessTypeById(businessTypeId, cancellationToken);
        if (existingBusinessType == null)
            throw new EntityNotFoundException(nameof(BusinessType), nameof(businessTypeId),
                businessTypeId.ToString());
        try
        {
            await businessTypesRepository.DeleteBusinessType(existingBusinessType, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(BusinessType), nameof(businessTypeId),
                businessTypeId.ToString());
        }
    }
}