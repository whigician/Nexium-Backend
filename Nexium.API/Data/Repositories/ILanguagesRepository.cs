using Nexium.API.Entities;

namespace Nexium.API.Data.Repositories;

public interface ILanguagesRepository
{
    public Task<List<Language>>
        GetAllLanguages(CancellationToken cancellationToken);

    public Task<Language> GetSingleLanguageByCode(string languageCode, CancellationToken cancellationToken);

    Task<Language> CreateLanguage(Language language, CancellationToken cancellationToken);
    Task UpdateLanguage(Language language, CancellationToken cancellationToken);
    Task DeleteLanguage(Language existingLanguage, CancellationToken cancellationToken);
}