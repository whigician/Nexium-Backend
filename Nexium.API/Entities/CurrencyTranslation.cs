using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class CurrencyTranslation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }

    [Required] [MaxLength(2)] public string LanguageCode { get; set; }

    [Required] [MaxLength(120)] public string TranslatedLabel { get; set; }

    public byte CurrencyId { get; set; }
    public Currency Currency { get; set; }
}