using Nexium.API.Entities;
using Nexium.API.TransferObjects.Language;
using Riok.Mapperly.Abstractions;

namespace Nexium.API.Configuration;

[Mapper]
public partial class LanguageMapper
{
    public partial Language MapToLanguage(LanguageSave currencySave);
    public partial LanguageView MapToLanguageView(Language currency);
}