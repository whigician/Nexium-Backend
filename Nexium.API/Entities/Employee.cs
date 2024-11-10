using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class Employee
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    public BusinessMember BusinessMember { get; set; }
    public long BusinessMemberId { get; set; }
    public DateTime HiringDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public UserAccount UserAccount { get; set; }
    public long? UserAccountId { get; set; }
    public ICollection<EmployeePosition> Positions { get; set; } = new List<EmployeePosition>();
}