using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.Industry;
using Nexium.API.TransferObjects.Translation;

namespace Nexium.API.Services.Implementation;

public class IndustriesService(
    IndustryMapper mapper,
    IIndustriesRepository industriesRepository,
    SelectedLanguageService selectedLanguageService,
    TranslationMapper industryTranslationMapper) : IIndustriesService
{
    public async Task<List<IndustryView>> GetAllIndustries(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var industries = await industriesRepository.GetAllIndustries(cancellationToken, selectedLanguage);
        return industries.Select(x => new IndustryView
        {
            Id = x.Id,
            Label = x.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ?? x.Label
        }).ToList();
    }

    public async Task<IndustryView> GetSingleIndustryById(short industryId, CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var industry =
            await industriesRepository.GetSingleIndustryById(industryId, cancellationToken, selectedLanguage, true);
        if (industry == null)
            throw new EntityNotFoundException(nameof(Industry), nameof(industryId), industryId.ToString());
        return new IndustryView
        {
            Id = industry.Id,
            Label = industry.Translations.FirstOrDefault(t => t.LanguageCode == selectedLanguage)?.TranslatedLabel ??
                    industry.Label
        };
    }

    public async Task<IndustryView> CreateIndustry(IndustrySave industryToCreate, CancellationToken cancellationToken)
    {
        var industry = mapper.MapToIndustry(industryToCreate);
        var createdIndustry = await industriesRepository.CreateIndustry(industry, cancellationToken);
        return mapper.MapToIndustryView(createdIndustry);
    }

    public async Task UpdateIndustry(short industryId, IndustrySave industryToUpdate,
        CancellationToken cancellationToken)
    {
        var industryUpdatedValues = mapper.MapToIndustry(industryToUpdate);
        var existingIndustry = await industriesRepository.GetSingleIndustryById(industryId, cancellationToken);
        if (existingIndustry == null)
            throw new EntityNotFoundException(nameof(Industry), nameof(industryId), industryId.ToString());
        existingIndustry.Label = industryUpdatedValues.Label;
        await industriesRepository.UpdateIndustry(existingIndustry, cancellationToken);
    }

    public async Task DeleteIndustry(short industryId, CancellationToken cancellationToken)
    {
        var existingIndustry = await industriesRepository.GetSingleIndustryById(industryId, cancellationToken);
        if (existingIndustry == null)
            throw new EntityNotFoundException(nameof(Industry), nameof(industryId), industryId.ToString());
        try
        {
            await industriesRepository.DeleteIndustry(existingIndustry, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(Industry), nameof(industryId), industryId.ToString());
        }
    }

    public async Task<List<TranslationView>> GetASingleIndustryTranslations(short industryId,
        CancellationToken cancellationToken)
    {
        return industryTranslationMapper.MapToIndustryTranslationViewList(
            await industriesRepository.GetASingleIndustryTranslations(industryId, cancellationToken, true));
    }

    public async Task UpdateASingleIndustryTranslations(short industryId,
        List<TranslationSave> industryTranslationsToSave, CancellationToken cancellationToken)
    {
        var existingTranslations =
            await industriesRepository.GetASingleIndustryTranslations(industryId, cancellationToken);
        var translationsToCreate =
            industryTranslationMapper.MapToIndustryTranslationList(industryTranslationsToSave
                .Where(x => x.Id is 0 or null).ToList());
        translationsToCreate.ForEach(t => t.IndustryId = industryId);
        var translationsUpdateInformation = industryTranslationsToSave
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
            .Where(et => industryTranslationsToSave.All(nt => nt.Id != et.Id && nt.Id != null && nt.Id != 0))
            .ToList();

        await SaveTranslations(translationsToCreate, translationsToUpdate, translationsToDelete, cancellationToken);
    }

    private async Task SaveTranslations(List<IndustryTranslation> translationsToCreate,
        List<IndustryTranslation> translationsToUpdate, List<IndustryTranslation> translationsToDelete,
        CancellationToken cancellationToken)
    {
        try
        {
            if (translationsToCreate.Count != 0)
                await industriesRepository.AddTranslations(translationsToCreate, cancellationToken);

            if (translationsToUpdate.Count != 0)
                await industriesRepository.UpdateTranslations(translationsToUpdate, cancellationToken);
        }
        catch (UniqueConstraintException)
        {
            throw new LanguageCodeAlreadyExists("", nameof(Industry));
        }

        if (translationsToDelete.Count != 0)
            await industriesRepository.RemoveTranslations(translationsToDelete, cancellationToken);
    }
}