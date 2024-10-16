﻿using System.ComponentModel.DataAnnotations;

namespace Nexium.API.Entities;

public class Language
{
    [Key] [MaxLength(2)] public string Code { get; set; }
    [Required] [MaxLength(25)] public string Name { get; set; }
}