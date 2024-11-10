using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class UserAccountActivity
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public DateTime OperationDate { get; set; } = DateTime.UtcNow;

    [Required] [MaxLength(250)] public string Operation { get; set; }

    [Required] [MaxLength(120)] public string EntityTypeName { get; set; }

    [Required] [MaxLength(30)] public string EntityId { get; set; }

    public UserAccount UserAccount { get; set; }
    public long UserAccountId { get; set; }
}