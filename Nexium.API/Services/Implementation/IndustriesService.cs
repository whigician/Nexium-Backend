using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.Industry;

namespace Nexium.API.Services.Implementation;

public class IndustriesService(
    IndustryMapper mapper,
    IIndustriesRepository industriesRepository,
    SelectedLanguageService selectedLanguageService,
    ITranslationMappingRepository translationMappingRepository) : IIndustriesService
{
    public async Task<List<IndustryView>> GetAllIndustries(CancellationToken cancellationToken)
    {
        var selectedLanguage = selectedLanguageService.GetSelectedLanguage();
        var industries = await industriesRepository.GetAllIndustries(cancellationToken, selectedLanguage);

        var industryViews = new List<IndustryView>();
        foreach (var x in industries)
        {
            var translatedLabel = (await translationMappingRepository.GetSingleEntityTranslationForAttribute(
                "Industry", x.Id, "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? x.Label;

            industryViews.Add(new IndustryView
            {
                Id = x.Id,
                Label = translatedLabel
            });
        }

        return industryViews;
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
            Label = (await translationMappingRepository.GetSingleEntityTranslationForAttribute("Industry", industry.Id,
                "Label", selectedLanguage, cancellationToken))?.TranslatedText ?? industry.Label
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
}