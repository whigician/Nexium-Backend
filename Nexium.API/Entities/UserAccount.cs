using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Nexium.API.Validators;

namespace Nexium.API.Entities;

[RequiredEmailOrPhone]
public class UserAccount
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [EmailAddress] [MaxLength(120)] public string Email { get; set; }

    [EmailAddress] [MaxLength(120)] public string BackupEmail { get; set; }

    [Phone] [MaxLength(120)] public string PhoneNumber { get; set; }

    [Required] [MaxLength(128)] public string HashedPassword { get; set; }

    public bool IsTwoFactorEnabled { get; set; }

    [Required] public TwoFactorMethod TwoFactorMethod { get; set; }

    public DateTime PasswordUpdatedAt { get; set; }
    public DateTime? LastLogin { get; set; }
    public byte FailedLoginAttempts { get; set; }
    public DateTime? AccountLockedUntil { get; set; }

    [MaxLength(250)] public string ProfilePictureUrl { get; set; }

    [MaxLength(120)] public string EmailVerificationCode { get; set; }

    public DateTime? EmailVerificationExpiry { get; set; }

    [MaxLength(120)] public string PhoneVerificationCode { get; set; }

    public DateTime? PhoneVerificationExpiry { get; set; }
    public bool EmailVerified { get; set; }
    public bool PhoneNumberVerified { get; set; }
    public BusinessOwner BusinessOwner { get; set; }
    public long? BusinessOwnerId { get; set; }
    public Employee Employee { get; set; }
    public long? EmployeeId { get; set; }
    public Business Business { get; set; }
    public long BusinessId { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public ICollection<UserAccountActivity> UserAccountActivities { get; set; } = new List<UserAccountActivity>();
    public ICollection<BusinessRole> BusinessRoles { get; set; } = new List<BusinessRole>();
    public ICollection<UserAccountPermission> UserAccountPermissions { get; set; } = new List<UserAccountPermission>();
}