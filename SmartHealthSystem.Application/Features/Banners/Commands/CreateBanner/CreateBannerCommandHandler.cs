using MediatR;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Commands.CreateBanner
{
    public class CreateBannerCommandHandler : IRequestHandler<CreateBannerCommandRequest, CreateBannerCommandResponse>
    {
        private readonly IPromoBannerRepository _bannerRepository;

        public CreateBannerCommandHandler(IPromoBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<CreateBannerCommandResponse> Handle(CreateBannerCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var banner = new PromoBanner
                {
                    Id = Guid.NewGuid(),
                    Title = request.Banner.Title,
                    Description = request.Banner.Description,
                    // Controller'da set ettiğimiz dosya yolunu alıyoruz
                    ImageUrl = request.Banner.ImageUrl,
                    TargetUrl = request.Banner.TargetUrl,
                    Order = request.Banner.Order,
                    IsActive = request.Banner.IsActive,
                    CreatedDate = DateTime.Now
                };

                await _bannerRepository.AddAsync(banner);

                return new CreateBannerCommandResponse
                {
                    Succeeded = true,
                    Message = "Banner başarıyla sisteme eklendi.",
                    BannerId = banner.Id
                };
            }
            catch (Exception ex)
            {
                return new CreateBannerCommandResponse
                {
                    Succeeded = false,
                    Message = $"Hata: {ex.Message}"
                };
            }
        }
    }
}
