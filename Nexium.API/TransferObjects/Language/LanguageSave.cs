using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.Language;

public class LanguageSave
{
    [Key] [MaxLength(2)] public string Code { get; set; }
    [Required] [MaxLength(25)] public string Name { get; set; }
}