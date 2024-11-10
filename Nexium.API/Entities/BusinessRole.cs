using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class BusinessRole
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [MaxLength(250)] public string Description { get; set; }

    public Business Business { get; set; }
    public long BusinessId { get; set; }
    public Role Role { get; set; }
    public byte RoleId { get; set; }
    public ICollection<UserAccount> UserAccounts { get; set; } = new List<UserAccount>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}