using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class Subscription
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public Business Business { get; set; }
    public long BusinessId { get; set; }
    public SubscriptionPlan SubscriptionPlan { get; set; }
    public ushort SubscriptionPlanId { get; set; }
    public DateTime StartsAt { get; set; }
    public DateTime EndsAt { get; set; }
    public bool AutoRenewal { get; set; }
    public SubscriptionStatus Status { get; set; }
}