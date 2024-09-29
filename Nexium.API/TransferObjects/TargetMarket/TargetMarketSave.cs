using System.ComponentModel.DataAnnotations;

namespace Nexium.API.TransferObjects.TargetMarket;

public class TargetMarketSave
{
    [Required] [MaxLength(50)] public string Label { get; set; }
}