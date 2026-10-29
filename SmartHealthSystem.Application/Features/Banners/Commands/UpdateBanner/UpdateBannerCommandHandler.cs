using MediatR;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Commands.UpdateBanner
{
    public class UpdateBannerCommandHandler : IRequestHandler<UpdateBannerCommandRequest, UpdateBannerCommandResponse>
    {
        private readonly IPromoBannerRepository _bannerRepository;

        public UpdateBannerCommandHandler(IPromoBannerRepository bannerRepository)
        {
            _bannerRepository = bannerRepository;
        }

        public async Task<UpdateBannerCommandResponse> Handle(UpdateBannerCommandRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // 1. Güncellenecek kaydı bul (UpdateBannerDto içinde Id olduğunu varsayıyorum)
                var banner = await _bannerRepository.GetBannerByIdAsync(request.UpdateBannerDto.Id);

                if (banner == null)
                {
                    return new UpdateBannerCommandResponse { Succeeded = false, Message = "Güncellenecek banner bulunamadı." };
                }

                // 2. Alanları güncelle
                banner.Title = request.UpdateBannerDto.Title;
                banner.TargetUrl = request.UpdateBannerDto.TargetUrl;
                banner.Order = request.UpdateBannerDto.Order;
                banner.IsActive = request.UpdateBannerDto.IsActive;
                banner.UpdatedDate = DateTime.Now; // Varsa UpdatedDate alanı

                // 3. Resim kontrolü: Eğer yeni resim yolu boş değilse (Controller'da kaydedilip sete edildiyse) güncelle
                if (!string.IsNullOrEmpty(request.UpdateBannerDto.ImageUrl))
                {
                    banner.ImageUrl = request.UpdateBannerDto.ImageUrl;
                }

                await _bannerRepository.UpdateAsync(banner);

                return new UpdateBannerCommandResponse
                {
                    Succeeded = true,
                    Message = "Banner başarıyla güncellendi."
                };
            }
            catch (Exception ex)
            {
                return new UpdateBannerCommandResponse
                {
                    Succeeded = false,
                    Message = $"Hata: {ex.Message}"
                };
            }
        }
    }
}
