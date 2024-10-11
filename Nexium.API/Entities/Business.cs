using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class Business
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Required] [MaxLength(255)] public string Name { get; set; }
    [MaxLength(120)]
    public string Alias { get; set; }
    [MaxLength(4)]
    public string EstablishmentYear { get; set; }
    [MaxLength(5)]
    public string StartOperatingHour { get; set; }
    [MaxLength(5)]
    public string EndOperatingHour { get; set; }
    public int EmployeesCount { get; set; }
    [MaxLength(120)]
    public string LogoPath { get; set; }
    public Months FiscalYearStartPeriod { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public List<Industry> Industries { get; set; }
    public List<TargetMarket> TargetMarkets { get; set; }
    public List<BusinessAddress> BusinessAddresses { get; set; }
    public List<BusinessContact> BusinessContacts { get; set; }
    public ICollection<BusinessRelationship> Suppliers { get; set; }
    public ICollection<BusinessRelationship> Retailers { get; set; }
    public Language Language { get; set; }
    [MaxLength(2)]
    public string LanguageCode { get; set; }
    public Currency Currency { get; set; }
    public byte LanguageId { get; set; }
    public BusinessType BusinessType { get; set; }
    public byte BusinessTypeId { get; set; }
    public BusinessStatus BusinessStatus { get; set; }
    public byte BusinessStatusId { get; set; }
    public List<BusinessIdentifier> BusinessIdentifiers { get; set; }
}