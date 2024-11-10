using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class Business
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [MaxLength(255)] public string Name { get; set; }
    [MaxLength(120)] public string Alias { get; set; }
    [MaxLength(120)] [EmailAddress] public string Email { get; set; }
    [MaxLength(120)] [Phone] public string PhoneNumber { get; set; }
    [MaxLength(4)] public string EstablishmentYear { get; set; }
    [MaxLength(5)] public string StartOperatingHour { get; set; }
    [MaxLength(5)] public string EndOperatingHour { get; set; }
    [MaxLength(3000)] public string AboutDescriptionMarkup { get; set; }
    public int EmployeesCount { get; set; }
    [MaxLength(220)] public string LogoUrl { get; set; }
    public Months FiscalYearStartMonth { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public bool IsRegistrationCompleted { get; set; }
    public byte AchievedStepIndex { get; set; }
    public DateTime TermsAcceptedAt { get; set; }
    public ICollection<Industry> Industries { get; set; } = new List<Industry>();
    public ICollection<TargetMarket> TargetMarkets { get; set; } = new List<TargetMarket>();
    public ICollection<BusinessAddress> BusinessAddresses { get; set; } = new List<BusinessAddress>();
    public ICollection<BusinessContact> BusinessContacts { get; set; } = new List<BusinessContact>();
    public ICollection<BusinessRelationship> Suppliers { get; set; } = new List<BusinessRelationship>();
    public ICollection<BusinessRelationship> Retailers { get; set; } = new List<BusinessRelationship>();
    public Language Language { get; set; }
    [MaxLength(2)] public string LanguageCode { get; set; }
    public Currency Currency { get; set; }
    public byte LanguageId { get; set; }
    public BusinessType BusinessType { get; set; }
    public byte BusinessTypeId { get; set; }
    public BusinessStatus CurrentStatus { get; set; }
    public byte CurrentStatusId { get; set; }
    public bool IsRegistered { get; set; }
    public ICollection<BusinessIdentifier> BusinessIdentifiers { get; set; } = new List<BusinessIdentifier>();
    public ICollection<BusinessLink> BusinessLinks { get; set; } = new List<BusinessLink>();
    public ICollection<BusinessRole> BusinessRoles { get; set; } = new List<BusinessRole>();
    public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();
}