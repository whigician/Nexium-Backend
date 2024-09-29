using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.BusinessStatus;

public class BusinessStatusSave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}