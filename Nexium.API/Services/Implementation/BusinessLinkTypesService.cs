using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects;
using Nexium.API.TransferObjects.BusinessLinkType;

namespace Nexium.API.Services.Implementation;

public class BusinessLinkTypesService(
    BusinessLinkTypeMapper mapper,
    IBusinessLinkTypesRepository businessLinkTypesRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository,
    IFileStorageService fileStorageService) : IBusinessLinkTypesService
{
    public async Task<List<BusinessLinkTypeView>> GetAllBusinessLinkTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessLinkTypes = await businessLinkTypesRepository.GetAllBusinessLinkTypes(cancellationToken);

        var businessLinkTypeViews = new List<BusinessLinkTypeView>();
        foreach (var x in businessLinkTypes)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "BusinessLinkType", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            businessLinkTypeViews.Add(new BusinessLinkTypeView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return businessLinkTypeViews;
    }

    public async Task<BusinessLinkTypeView> GetSingleBusinessLinkTypeById(byte businessLinkTypeId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessLinkType =
            await businessLinkTypesRepository.GetSingleBusinessLinkTypeById(businessLinkTypeId, cancellationToken,
                true);
        if (businessLinkType == null)
            throw new EntityNotFoundException(nameof(BusinessLinkType), nameof(businessLinkTypeId),
                businessLinkTypeId.ToString());
        return new BusinessLinkTypeView
        {
            Id = businessLinkType.Id,
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("BusinessLinkType",
                        businessLinkType.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ??
                    businessLinkType.Label
        };
    }

    public async Task<BusinessLinkTypeView> CreateBusinessLinkType(BusinessLinkTypeSave businessLinkTypeToCreate,
        CancellationToken cancellationToken)
    {
        var businessLinkType = mapper.MapToBusinessLinkType(businessLinkTypeToCreate);
        var createdBusinessLinkType =
            await businessLinkTypesRepository.CreateBusinessLinkType(businessLinkType, cancellationToken);
        return mapper.MapToBusinessLinkTypeView(createdBusinessLinkType);
    }

    public async Task UpdateBusinessLinkType(byte businessLinkTypeId, BusinessLinkTypeSave businessLinkTypeToUpdate,
        CancellationToken cancellationToken)
    {
        var businessLinkTypeUpdatedValues = mapper.MapToBusinessLinkType(businessLinkTypeToUpdate);
        var existingBusinessLinkType =
            await businessLinkTypesRepository.GetSingleBusinessLinkTypeById(businessLinkTypeId, cancellationToken);
        if (existingBusinessLinkType == null)
            throw new EntityNotFoundException(nameof(BusinessLinkType), nameof(businessLinkTypeId),
                businessLinkTypeId.ToString());
        existingBusinessLinkType.Label = businessLinkTypeUpdatedValues.Label;
        await businessLinkTypesRepository.UpdateBusinessLinkType(existingBusinessLinkType, cancellationToken);
    }

    public async Task DeleteBusinessLinkType(byte businessLinkTypeId, CancellationToken cancellationToken)
    {
        var existingBusinessLinkType =
            await businessLinkTypesRepository.GetSingleBusinessLinkTypeById(businessLinkTypeId, cancellationToken);
        if (existingBusinessLinkType == null)
            throw new EntityNotFoundException(nameof(BusinessLinkType), nameof(businessLinkTypeId),
                businessLinkTypeId.ToString());
        try
        {
            await businessLinkTypesRepository.DeleteBusinessLinkType(existingBusinessLinkType, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(BusinessLinkType), nameof(businessLinkTypeId),
                businessLinkTypeId.ToString());
        }
    }

    public async Task<UploadResponse> UploadLogo(byte businessLinkTypeId, IFormFile logo)
    {
        var businessLinkType =
            await businessLinkTypesRepository.GetSingleBusinessLinkTypeById(businessLinkTypeId, CancellationToken.None);
        if (businessLinkType == null)
            throw new EntityNotFoundException(nameof(BusinessLinkType), "id", businessLinkTypeId.ToString());
        var logoPaths =
            await fileStorageService.SaveOrReplaceLogoAsync(logo, nameof(BusinessLinkType),
                businessLinkTypeId.ToString(), true);
        businessLinkType.OriginalLogoUrl = logoPaths.OriginalLogoPath;
        businessLinkType.ResizedLogoUrl = logoPaths.ResizedLogoPath;
        await businessLinkTypesRepository.UpdateBusinessLinkType(businessLinkType, CancellationToken.None);
        return new UploadResponse
        {
            FileName = logo.FileName,
            OriginalFilePath = businessLinkType.OriginalLogoUrl,
            ResizedFilePath = businessLinkType.ResizedLogoUrl,
            Success = true
        };
    }
}