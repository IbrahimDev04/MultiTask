﻿using System.ComponentModel.DataAnnotations;

namespace MultiShop.Models;
public class Slider : BaseEntity
{
    [MaxLength(32, ErrorMessage = "Başlıq 32 simvoldun artıq ola bilməz"), Required]
    public string Title { get; set; }

    [MaxLength(64, ErrorMessage = "Description 64 simvoldun artıq ola bilməz"), Required]
    public string SubTitle { get; set; }

    [Required]
    public string İmageUrl { get; set; }
}