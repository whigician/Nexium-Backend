using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nexium.API.Validators;

namespace Nexium.API.Entities;

[RequiredEmployeeOrBusinessOwner]
public class BusinessMember
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [MaxLength(50)] public string FirstName { get; set; }

    [Required] [MaxLength(50)] public string LastName { get; set; }

    [MaxLength(120)] [EmailAddress] public string Email { get; set; }
    [MaxLength(120)] [Phone] public string PhoneNumber { get; set; }
    [MaxLength(250)] public string Address { get; set; }
    public City City { get; set; }
    public ushort? CityId { get; set; }

    [MaxLength(250)] public string PhotoUrl { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public Business Business { get; set; }
    public long BusinessId { get; set; }
    public BusinessOwner BusinessOwner { get; set; }
    public long? BusinessOwnerId { get; set; }
    public Employee Employee { get; set; }
    public long? EmployeeId { get; set; }
    public ICollection<PersonIdentifier> PersonIdentifiers { get; set; } = new List<PersonIdentifier>();
}