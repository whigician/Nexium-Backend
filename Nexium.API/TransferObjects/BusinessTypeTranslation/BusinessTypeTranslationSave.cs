using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.BusinessTypeTranslation;

public class BusinessTypeTranslationSave
{
    public long? Id { get; set; }

    [Required] [MaxLength(10)] public string LanguageCode { get; set; }

    [Required] [MaxLength(120)] public string TranslatedLabel { get; set; }
}