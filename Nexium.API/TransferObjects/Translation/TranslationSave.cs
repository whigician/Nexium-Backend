using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.Translation;

public class TranslationSave
{
    public long? Id { get; set; }

    [Required] [MaxLength(10)] public string LanguageCode { get; set; }

    [Required] [MaxLength(120)] public string TranslatedLabel { get; set; }
}