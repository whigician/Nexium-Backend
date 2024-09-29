using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.BusinessType;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services.Implementation;

public class BusinessTypesService(
    BusinessTypeMapper mapper,
    IBusinessTypesRepository businessTypesRepository,
    SelectedLanguageService selectedLanguageService,
    TranslationMapper translationMapper) : IBusinessTypesService
{
    public async Task<List<BusinessTypeView>> GetAllBusinessTypes(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessTypes = await businessTypesRepository.GetAllBusinessTypes(cancellationToken, selectedLanguage);
        return businessTypes.Select(x => new BusinessTypeView
        {
            Id = x.Id,
            Label = x.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ?? x.Label
        }).ToList();
    }

    public async Task<BusinessTypeView> GetSingleBusinessTypeById(byte businessTypeId,
        CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var businessType =
            await businessTypesRepository.GetSingleBusinessTypeById(businessTypeId, cancellationToken, selectedLanguage,
                true);
        if (businessType == null)
            throw new EntityNotFoundException(nameof(BusinessType), nameof(businessTypeId), businessTypeId.ToString());
        return new BusinessTypeView
        {
            Id = businessType.Id,
            Label =
                businessType.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ??
                businessType.Label
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

    public async Task<List<TranslationView>> GetASingleBusinessTypeTranslations(byte businessTypeId,
        CancellationToken cancellationToken)
    {
        return translationMapper.MapToBusinessTypeTranslationViewList(
            await businessTypesRepository.GetASingleBusinessTypeTranslations(businessTypeId, cancellationToken, true));
    }

    public async Task UpdateASingleBusinessTypeTranslations(byte businessTypeId,
        List<TranslationSave> businessTypeTranslationsToSave, CancellationToken cancellationToken)
    {
        var existingTranslations =
            await businessTypesRepository.GetASingleBusinessTypeTranslations(businessTypeId, cancellationToken);
        var translationsToCreate =
            translationMapper.MapToBusinessTypeTranslationList(businessTypeTranslationsToSave
                .Where(x => x.Id is 0 or null).ToList());
        translationsToCreate.ForEach(t => t.BusinessTypeId = businessTypeId);
        var translationsUpdateInformation = businessTypeTranslationsToSave
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
            .Where(et => businessTypeTranslationsToSave.All(nt => nt.Id != et.Id && nt.Id != null && nt.Id != 0))
            .ToList();

        await SaveTranslations(translationsToCreate, translationsToUpdate, translationsToDelete, cancellationToken);
    }

    private async Task SaveTranslations(List<BusinessTypeTranslation> translationsToCreate,
        List<BusinessTypeTranslation> translationsToUpdate, List<BusinessTypeTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        try
        {
            if (translationsToCreate.Count != 0)
                await businessTypesRepository.AddTranslations(translationsToCreate, cancellationToken);

            if (translationsToUpdate.Count != 0)
                await businessTypesRepository.UpdateTranslations(translationsToUpdate, cancellationToken);
        }
        catch (UniqueConstraintException ex)
        {
            throw new LanguageCodeAlreadyExists("", nameof(BusinessType));
        }

        if (translationsToDelete.Count != 0)
            await businessTypesRepository.RemoveTranslations(translationsToDelete, cancellationToken);
    }
}