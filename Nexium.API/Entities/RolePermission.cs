using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class RolePermission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public AppSection AppSection { get; set; }
    public ushort AppSectionId { get; set; }
    public BusinessRole BusinessRole { get; set; }
    public long BusinessRoleId { get; set; }
    public Permission Permission { get; set; }
    public ushort PermissionId { get; set; }
    public bool Allowed { get; set; }
}