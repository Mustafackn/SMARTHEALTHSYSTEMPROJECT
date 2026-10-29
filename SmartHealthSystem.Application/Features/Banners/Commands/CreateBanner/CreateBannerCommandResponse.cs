using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Commands.CreateBanner
{
    public class CreateBannerCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
        public Guid? BannerId { get; set; } // Oluşturulan kaydın ID'si
    }
}
