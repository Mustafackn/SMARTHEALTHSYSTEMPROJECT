using MediatR;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.DTOs.BannerDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Queries.GetBannerById
{
    public class GetBannerByIdQueryHandler : IRequestHandler<GetBannerByIdQueryRequest, GetBannerByIdQueryResponse>
    {
        private readonly IPromoBannerRepository _bannerRepository;

        public GetBannerByIdQueryHandler(IPromoBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<GetBannerByIdQueryResponse> Handle(GetBannerByIdQueryRequest request, CancellationToken cancellationToken)
        {
            // Daha önce repository'de oluşturduğumuz özel metodu çağırıyoruz
            var banner = await _bannerRepository.GetBannerByIdAsync(request.Id);

            if (banner == null) return new GetBannerByIdQueryResponse { Banner = null };

            return new GetBannerByIdQueryResponse
            {
                Banner = new UpdateBannerDto
                {
                    Id = banner.Id,
                    Title = banner.Title,
                    ImageUrl = banner.ImageUrl,
                    TargetUrl = banner.TargetUrl,
                    IsActive = banner.IsActive,
                    Order = banner.Order
                }
            };
        }
    }
}
