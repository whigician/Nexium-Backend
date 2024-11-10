using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class Permission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ushort Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; }

    [MaxLength(250)] public string Description { get; set; }

    public PermissionType PermissionType { get; set; }
    public ICollection<UserAccountPermission> UserAccountPermissions { get; set; } = new List<UserAccountPermission>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
}