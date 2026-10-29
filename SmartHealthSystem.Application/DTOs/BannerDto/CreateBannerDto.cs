using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.BannerDto
{
    public class CreateBannerDto
    {
        public string Title { get; set; }
        public string? Description { get; set; }

        // Veritabanına kaydedilecek string yol (Handler tarafında set edilecek)
        public string? ImageUrl { get; set; }

        // Bilgisayardan seçilen gerçek dosya (Sadece UI'dan Controller'a veri taşır)
        public IFormFile? ImageFile { get; set; }

        public string? TargetUrl { get; set; }
        public int Order { get; set; }
        public bool IsActive { get; set; } = true;
    }
}
