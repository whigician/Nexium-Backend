using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class Business
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [MaxLength(255)] public string Name { get; set; }

    public string Alias { get; set; }
    public string EstablishmentYear { get; set; }
    public string StartOperatingHour { get; set; }
    public string EndOperatingHour { get; set; }
    public int EmployeesCount { get; set; }
    public string LogoPath { get; set; }
    public Months FiscalYearStartPeriod { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<Industry> Industries { get; set; }
    public List<TargetMarket> TargetMarkets { get; set; }
    public List<BusinessAddress> BusinessAddresses { get; set; }
    public List<BusinessContact> BusinessContacts { get; set; }

    public List<BusinessCustomAttribute> BusinessCustomAttributes { get; set; }

// Navigation properties to the join table
    public ICollection<BusinessRelationship> Suppliers { get; set; }
    public ICollection<BusinessRelationship> Retailers { get; set; }
    public Language Language { get; set; }
    public string LanguageCode { get; set; }
    public Currency Currency { get; set; }
    public byte LanguageId { get; set; }
    public BusinessType BusinessType { get; set; }
    public byte BusinessTypeId { get; set; }
    public BusinessStatus BusinessStatus { get; set; }
    public sbyte BusinessStatusId { get; set; }
    public List<BusinessIdentifier> BusinessIdentifiers { get; set; }
}