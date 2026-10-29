using MediatR;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.DTOs.BannerDto;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Queries.GetAllBanners
{
    public class GetAllBannersQueryHandler : IRequestHandler<GetAllBannersQueryRequest, GetAllBannersQueryResponse>
    {
        private readonly IPromoBannerRepository _bannerRepository;

        public GetAllBannersQueryHandler(IPromoBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<GetAllBannersQueryResponse> Handle(GetAllBannersQueryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Veritabanından tüm bannerları çek
                var banners = await _bannerRepository.GetAllAsync();

                // 2. Entity listesini DTO listesine dönüştür (Mapping)
                var bannerList = banners.Select(x => new GetBannerDto
                {
                    Id = x.Id,
                    Title = x.Title,
                    Description = x.Description,
                    ImageUrl = x.ImageUrl,
                    TargetUrl = x.TargetUrl,
                    Order = x.Order,
                    IsActive = x.IsActive
                }).OrderBy(x => x.Order).ToList();

                // 3. Başarılı sonucu Response ile paketle
                return new GetAllBannersQueryResponse
                {
                    Succeeded = true,
                    Banners = bannerList,
                    Message = "Banner listesi başarıyla getirildi."
                };
            }
            catch (Exception ex)
            {
                // 4. Hata durumunda loglama yapılabilir ve güvenli hata mesajı dönülür
                return new GetAllBannersQueryResponse
                {
                    Succeeded = false,
                    Banners = new List<GetBannerDto>(), // Boş liste dönerek null hatasını engelleriz
                    Message = $"Bannerlar yüklenirken bir sorun oluştu: {ex.Message}"
                };
            }
        }
    }
}
