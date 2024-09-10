using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class BusinessContact
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [MaxLength(50)] public string ResponsibleName { get; set; }

    [MaxLength(50)] public string ResponsiblePosition { get; set; }

    [MaxLength(320)] public string Email { get; set; }

    [MaxLength(15)] public string Phone { get; set; }

    public bool IsPrimary { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ContactType ContactType { get; set; }
    public sbyte ContactTypeId { get; set; }
    public Business Business { get; set; }
    public long BusinessId { get; set; }
}