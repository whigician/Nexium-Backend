using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.BusinessStatus;

namespace Nexium.API.Services.Implementation;

public class BusinessStatusesService(
    BusinessStatusMapper mapper,
    IBusinessStatusesRepository businessStatusesRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : IBusinessStatusesService
{
    public async Task<List<BusinessStatusView>> GetAllBusinessStatuses(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessStatuses =
            await businessStatusesRepository.GetAllBusinessStatuses(cancellationToken, selectedLanguage);

        var businessStatusViews = new List<BusinessStatusView>();
        foreach (var x in businessStatuses)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "BusinessStatus", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            businessStatusViews.Add(new BusinessStatusView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return businessStatusViews;
    }

    public async Task<BusinessStatusView> GetSingleBusinessStatusById(byte businessStatusId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessStatus =
            await businessStatusesRepository.GetSingleBusinessStatusById(businessStatusId, cancellationToken,
                selectedLanguage, true);
        if (businessStatus == null)
            throw new EntityNotFoundException(nameof(BusinessStatus), nameof(businessStatusId),
                businessStatusId.ToString());
        return new BusinessStatusView
        {
            Id = businessStatus.Id,
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("BusinessStatus",
                businessStatus.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? businessStatus.Label
        };
    }

    public async Task<BusinessStatusView> CreateBusinessStatus(BusinessStatusSave businessStatusToCreate,
        CancellationToken cancellationToken)
    {
        var businessStatus = mapper.MapToBusinessStatus(businessStatusToCreate);
        var createdBusinessStatus =
            await businessStatusesRepository.CreateBusinessStatus(businessStatus, cancellationToken);
        return mapper.MapToBusinessStatusView(createdBusinessStatus);
    }

    public async Task UpdateBusinessStatus(byte businessStatusId, BusinessStatusSave businessStatusToUpdate,
        CancellationToken cancellationToken)
    {
        var businessStatusUpdatedValues = mapper.MapToBusinessStatus(businessStatusToUpdate);
        var existingBusinessStatus =
            await businessStatusesRepository.GetSingleBusinessStatusById(businessStatusId, cancellationToken);
        if (existingBusinessStatus == null)
            throw new EntityNotFoundException(nameof(BusinessStatus), nameof(businessStatusId),
                businessStatusId.ToString());
        existingBusinessStatus.Label = businessStatusUpdatedValues.Label;
        await businessStatusesRepository.UpdateBusinessStatus(existingBusinessStatus, cancellationToken);
    }

    public async Task DeleteBusinessStatus(byte businessStatusId, CancellationToken cancellationToken)
    {
        var existingBusinessStatus =
            await businessStatusesRepository.GetSingleBusinessStatusById(businessStatusId, cancellationToken);
        if (existingBusinessStatus == null)
            throw new EntityNotFoundException(nameof(BusinessStatus), nameof(businessStatusId),
                businessStatusId.ToString());
        try
        {
            await businessStatusesRepository.DeleteBusinessStatus(existingBusinessStatus, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(BusinessStatus), nameof(businessStatusId),
                businessStatusId.ToString());
        }
    }
}