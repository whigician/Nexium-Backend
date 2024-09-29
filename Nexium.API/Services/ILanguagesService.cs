using Nexium.API.TransferObjects.Language;

namespace Nexium.API.Services;

public interface ILanguagesService
{
    public Task<List<LanguageView>> GetAllLanguages(CancellationToken cancellationToken);
    public Task<LanguageView> GetSingleLanguageByCode(string languageCode, CancellationToken cancellationToken);
    public Task<LanguageView> CreateLanguage(LanguageSave languageToCreate, CancellationToken cancellationToken);
    public Task UpdateLanguage(LanguageSave languageToUpdate, CancellationToken cancellationToken);
    public Task DeleteLanguage(string languageCode, CancellationToken cancellationToken);
}