using SmartHealthSystem.Application.DTOs.BannerDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Queries.GetBannerById
{
    public class GetBannerByIdQueryResponse
    {
        // View'a gidecek verileri taşıyan DTO (Senin Update sayfasında kullandığın Banner nesnesi)
        public UpdateBannerDto Banner { get; set; }
    }
}
