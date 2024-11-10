using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class UserAccountPermission
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public AppSection AppSection { get; set; }
    public ushort AppSectionId { get; set; }
    public UserAccount UserAccount { get; set; }
    public ushort PermissionId { get; set; }
    public Permission Permission { get; set; }
    public long UserAccountId { get; set; }
    public bool Allowed { get; set; }
}