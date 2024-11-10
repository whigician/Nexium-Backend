using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.EmployeePosition;

public class EmployeePositionSave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}