using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.BannerDto
{
    public class UpdateBannerDto : BaseDto
    {
        public string Title { get; set; }
        public string? ImageUrl { get; set; } // Mevcut resmin yolu
        public IFormFile? ImageFile { get; set; } // Yeni seçilen resim (varsa)
        public string? TargetUrl { get; set; }
        public bool IsActive { get; set; }
        public int Order { get; set; } // Güncellemede sıra da önemli olabilir
    }
}
