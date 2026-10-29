using MediatR;
using SmartHealthSystem.Application.DTOs.BannerDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Commands.UpdateBanner
{
    public class UpdateBannerCommandRequest : IRequest<UpdateBannerCommandResponse>
    {
        public UpdateBannerDto UpdateBannerDto { get; set; }
    }
}
