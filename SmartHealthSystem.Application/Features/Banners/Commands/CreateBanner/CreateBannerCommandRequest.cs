using MediatR;
using SmartHealthSystem.Application.DTOs.BannerDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Commands.CreateBanner
{
    public class CreateBannerCommandRequest : IRequest<CreateBannerCommandResponse>
    {
        // Ayrı ayrı property yazmak yerine doğrudan DTO'yu gömüyoruz
        public CreateBannerDto Banner { get; set; }
    }
}
