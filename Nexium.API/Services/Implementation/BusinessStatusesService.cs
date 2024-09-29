using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.BusinessStatus;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services.Implementation;

public class BusinessStatusesService(
    BusinessStatusMapper mapper,
    IBusinessStatusesRepository businessStatusesRepository,
    SelectedLanguageService selectedLanguageService,
    TranslationMapper translationMapper) : IBusinessStatusesService
{
    public async Task<List<BusinessStatusView>> GetAllBusinessStatuses(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessStatuses =
            await businessStatusesRepository.GetAllBusinessStatuses(cancellationToken, selectedLanguage);
        return businessStatuses.Select(x => new BusinessStatusView
        {
            Id = x.Id,
            Label = x.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ?? x.Label
        }).ToList();
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
            Label =
                businessStatus.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ??
                businessStatus.Label
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

    public async Task<List<TranslationView>> GetASingleBusinessStatusTranslations(byte businessStatusId,
        CancellationToken cancellationToken)
    {
        return translationMapper.MapToBusinessStatusTranslationViewList(
            await businessStatusesRepository.GetASingleBusinessStatusTranslations(businessStatusId, cancellationToken,
                true));
    }

    public async Task UpdateASingleBusinessStatusTranslations(byte businessStatusId,
        List<TranslationSave> businessStatusTranslationsToSave, CancellationToken cancellationToken)
    {
        var existingTranslations =
            await businessStatusesRepository.GetASingleBusinessStatusTranslations(businessStatusId, cancellationToken);
        var translationsToCreate =
            translationMapper.MapToBusinessStatusTranslationList(businessStatusTranslationsToSave
                .Where(x => x.Id is 0 or null).ToList());
        translationsToCreate.ForEach(t => t.BusinessStatusId = businessStatusId);
        var translationsUpdateInformation = businessStatusTranslationsToSave
            .Where(x => existingTranslations.Any(i => i.Id == x.Id)).ToList();
        var translationsToUpdate = translationsUpdateInformation.Select(x =>
        {
            var translationToBeUpdated = existingTranslations.First(et => et.Id == x.Id);
            if (translationToBeUpdated.LanguageCode == x.LanguageCode &&
                translationToBeUpdated.TranslatedLabel == x.TranslatedLabel) return null;
            translationToBeUpdated.LanguageCode = x.LanguageCode;
            translationToBeUpdated.TranslatedLabel = x.TranslatedLabel;
            return translationToBeUpdated;
        }).Where(x => x != null).ToList();
        var translationsToDelete = existingTranslations
            .Where(et => businessStatusTranslationsToSave.All(nt => nt.Id != et.Id && nt.Id != null && nt.Id != 0))
            .ToList();

        await SaveTranslations(translationsToCreate, translationsToUpdate, translationsToDelete, cancellationToken);
    }

    private async Task SaveTranslations(List<BusinessStatusTranslation> translationsToCreate,
        List<BusinessStatusTranslation> translationsToUpdate, List<BusinessStatusTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        try
        {
            if (translationsToCreate.Count != 0)
                await businessStatusesRepository.AddTranslations(translationsToCreate, cancellationToken);

            if (translationsToUpdate.Count != 0)
                await businessStatusesRepository.UpdateTranslations(translationsToUpdate, cancellationToken);
        }
        catch (UniqueConstraintException ex)
        {
            throw new LanguageCodeAlreadyExists("", nameof(BusinessStatus));
        }

        if (translationsToDelete.Count != 0)
            await businessStatusesRepository.RemoveTranslations(translationsToDelete, cancellationToken);
    }
}