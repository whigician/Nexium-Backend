using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class BusinessOwner
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public BusinessMember BusinessMember { get; set; }
    public long BusinessMemberId { get; set; }
    public float OwnerShipPercentage { get; set; }
    public DateOnly OwnerShipStartDate { get; set; }
    public bool IsPrimaryOwner { get; set; }
    public UserAccount UserAccount { get; set; }
    public long? UserAccountId { get; set; }
}