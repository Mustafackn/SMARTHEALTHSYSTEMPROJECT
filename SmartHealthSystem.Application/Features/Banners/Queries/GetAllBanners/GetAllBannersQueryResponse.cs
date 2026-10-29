using SmartHealthSystem.Application.DTOs.BannerDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Queries.GetAllBanners
{
    public class GetAllBannersQueryResponse
    {
        public bool Succeeded { get; set; }
        public List<GetBannerDto> Banners { get; set; }
        public string Message { get; set; }
    }
}
