using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.BusinessType;

public class BusinessTypeSave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}