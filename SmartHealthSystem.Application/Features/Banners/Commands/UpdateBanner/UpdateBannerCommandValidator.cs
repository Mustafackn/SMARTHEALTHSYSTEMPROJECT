using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Commands.UpdateBanner
{
    public class UpdateBannerCommandValidator : AbstractValidator<UpdateBannerCommandRequest>
    {
        public UpdateBannerCommandValidator()
        {
            RuleFor(x => x.UpdateBannerDto.Id)
                .NotEmpty().WithMessage("Güncellenecek kaydın ID bilgisi zorunludur.");

            RuleFor(x => x.UpdateBannerDto.Title)
                .NotEmpty().WithMessage("Banner başlığı boş geçilemez.")
                .MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");

            // Not: ImageFile için güncellemede .NotNull() demiyoruz çünkü kullanıcı eski resmi kullanmaya devam edebilir.

            RuleFor(x => x.UpdateBannerDto.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Sıralama değeri negatif olamaz.");
        }
    }
}
