using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.Banners.Commands.CreateBanner
{
    public class CreateBannerCommandValidator : AbstractValidator<CreateBannerCommandRequest>
    {
        public CreateBannerCommandValidator()
        {
            RuleFor(x => x.Banner.Title)
                .NotEmpty().WithMessage("Banner başlığı boş geçilemez.")
                .MaximumLength(150).WithMessage("Başlık en fazla 150 karakter olabilir.");

            // ImageUrl yerine ImageFile kontrolü yapıyoruz (Yeni kayıt için zorunlu)
            RuleFor(x => x.Banner.ImageFile)
                .NotNull().WithMessage("Lütfen bir görsel dosyası seçiniz.");

            RuleFor(x => x.Banner.Description)
                .MaximumLength(500).WithMessage("Açıklama 500 karakterden uzun olamaz.");

            RuleFor(x => x.Banner.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Sıralama değeri negatif olamaz.");
        }
    }
}
