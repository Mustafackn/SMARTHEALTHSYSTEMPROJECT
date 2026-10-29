using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.BannerDto
{
    public abstract class BaseDto
    {
        public Guid Id { get; set; }

        // İsteğe bağlı: Her DTO'da tarih görmek istersen ekleyebilirsin
        // public DateTime CreatedDate { get; set; }
    }
}
