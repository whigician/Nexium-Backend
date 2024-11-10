using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class SubscriptionPlan
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ushort Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; }

    public decimal MonthlyPricePerUser { get; set; }
    public decimal YearlyPricePerUser { get; set; }
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}