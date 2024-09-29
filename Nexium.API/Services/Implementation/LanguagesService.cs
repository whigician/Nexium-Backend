using EntityFramework.Exceptions.Common;
using Nexium.API.Configuration;
using Nexium.API.Data.Repositories;
using Nexium.API.Entities;
using Nexium.API.Exceptions;
using Nexium.API.TransferObjects.Language;

namespace Nexium.API.Services.Implementation;

public class LanguagesService(
    LanguageMapper mapper,
    ILanguagesRepository languagesRepository) : ILanguagesService
{
    public async Task<List<LanguageView>> GetAllLanguages(CancellationToken cancellationToken)
    {
        var languages =
            await languagesRepository.GetAllLanguages(cancellationToken);
        return languages.Select(x => new LanguageView
        {
            Code = x.Code,
            Name = x.Name
        }).ToList();
    }

    public async Task<LanguageView> GetSingleLanguageByCode(string languageCode,
        CancellationToken cancellationToken)
    {
        var language =
            await languagesRepository.GetSingleLanguageByCode(languageCode, cancellationToken);
        if (language == null)
            throw new EntityNotFoundException(nameof(Language), nameof(languageCode),
                languageCode);
        return new LanguageView
        {
            Code = language.Code,
            Name = language.Name
        };
    }

    public async Task<LanguageView> CreateLanguage(LanguageSave languageToCreate,
        CancellationToken cancellationToken)
    {
        var language = mapper.MapToLanguage(languageToCreate);
        var createdLanguage =
            await languagesRepository.CreateLanguage(language, cancellationToken);
        return mapper.MapToLanguageView(createdLanguage);
    }

    public async Task UpdateLanguage(LanguageSave languageToUpdate, CancellationToken cancellationToken)
    {
        var languageUpdatedValues = mapper.MapToLanguage(languageToUpdate);
        var existingLanguage =
            await languagesRepository.GetSingleLanguageByCode(languageToUpdate.Code, cancellationToken);
        if (existingLanguage == null)
            throw new EntityNotFoundException(nameof(Language), nameof(languageToUpdate.Code),
                languageToUpdate.Code);
        existingLanguage.Name = languageUpdatedValues.Name;
        await languagesRepository.UpdateLanguage(existingLanguage, cancellationToken);
    }

    public async Task DeleteLanguage(string languageCode, CancellationToken cancellationToken)
    {
        var existingLanguage =
            await languagesRepository.GetSingleLanguageByCode(languageCode, cancellationToken);
        if (existingLanguage == null)
            throw new EntityNotFoundException(nameof(Language), nameof(languageCode),
                languageCode);
        try
        {
            await languagesRepository.DeleteLanguage(existingLanguage, cancellationToken);
        }
        catch (ReferenceConstraintException)
        {
            throw new EntityReferencedException(nameof(Language), nameof(languageCode),
                languageCode);
        }
    }
}