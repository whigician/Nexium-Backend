using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class TranslationMapping
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int Id { get; set; }

    public Language Language { get; set; }
    [Required] [MaxLength(2)] public string LanguageCode { get; set; }
    [MaxLength(50)] public string EntityTypeName { get; set; }
    public long EntityId { get; set; }
    [MaxLength(50)] public string AttributeName { get; set; }
    [Required] [MaxLength(120)] public string TranslatedText { get; set; }
}