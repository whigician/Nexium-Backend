namespace Nexium.API.TransferObjects.Translation;

public class TranslationMappingView
{
    public long Id { get; set; }
    public string LanguageCode { get; set; }
    public string TranslatedText { get; set; }
}