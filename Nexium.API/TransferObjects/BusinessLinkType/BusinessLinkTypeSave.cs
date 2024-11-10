using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.BusinessLinkType;

public class BusinessLinkTypeSave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}