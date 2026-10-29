using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.BannerDto
{
    public class GetBannerDto : BaseDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public string ImageUrl { get; set; } // Ekranda <img> tag'ine basılacak yol
        public string? TargetUrl { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; }
    }
}
