using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class AppSection
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ushort Id { get; set; }

    [Required] [MaxLength(120)] public string Label { get; set; }

    public AppSection ParentAppSection { get; set; }
    public ushort? ParentAppSectionId { get; set; }
    public ICollection<AppSection> ChildSections { get; set; } = new List<AppSection>();
    public ICollection<RolePermission> RolePermissions { get; set; } = new List<RolePermission>();
    public ICollection<UserAccountPermission> UserAccountPermissions { get; set; } = new List<UserAccountPermission>();
}