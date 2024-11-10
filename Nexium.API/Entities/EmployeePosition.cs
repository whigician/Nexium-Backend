using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class EmployeePosition
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ushort Id { get; set; }

    [MaxLength(50)] public string Label { get; set; }

    public ICollection<Employee> Employees { get; set; } = new List<Employee>();
    public ICollection<BusinessContact> BusinessContacts { get; set; } = new List<BusinessContact>();
}