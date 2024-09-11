using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class BusinessTypeTranslation
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public short Id { get; set; }

    [Required] [MaxLength(10)] public string LanguageCode { get; set; }

    [Required] [MaxLength(120)] public string TranslatedLabel { get; set; }

    public byte BusinessTypeId { get; set; }
    public BusinessType BusinessType { get; set; }
}