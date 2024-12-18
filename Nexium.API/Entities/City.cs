﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Nexium.API.Entities;

public class City
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public ushort Id { get; set; }

    [Required] [MaxLength(50)] public string Name { get; set; }

    public Country Country { get; set; }
    public byte CountryId { get; set; }
    public ICollection<BusinessAddress> BusinessAddresses { get; set; } = new List<BusinessAddress>();
}