using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class BusinessAddress
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [MaxLength(255)] public string Address { get; set; }

    [MaxLength(10)] public string ZipCode { get; set; }

    [MaxLength(25)] public string Longitude { get; set; }

    [MaxLength(25)] public string Latitude { get; set; }

    public bool IsPrimary { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    public DateTime? UpdatedAt { get; set; }
    public DateTime? DeletedAt { get; set; }
    public AddressType AddressType { get; set; }
    public byte AddressTypeId { get; set; }
    public Business Business { get; set; }
    public long BusinessId { get; set; }
    public City City { get; set; }
    public ushort CityId { get; set; }
}