using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.UpdateHeartTestData
{
    public class UpdateHeartTestDataCommandValidator : AbstractValidator<UpdateHeartTestDataCommandRequest>
    {
        public UpdateHeartTestDataCommandValidator()
        {
            // 0. ID ve Hasta Kontrolü
            RuleFor(p => p.UpdateHeartTestDataDto.Id)
                .NotEmpty().WithMessage("Güncellenecek analiz ID'si boş olamaz.");

            RuleFor(p => p.UpdateHeartTestDataDto.PatientId)
                .NotEmpty().WithMessage("Analizin hangi hastaya ait olduğu belirtilmelidir.");

            // 1. Temel Demografik Veriler (Senin kriterlerinle)
            RuleFor(p => p.UpdateHeartTestDataDto.Age)
                .NotEmpty().WithMessage("Yaş alanı boş geçilemez.")
                .InclusiveBetween(1, 120).WithMessage("Yaş 1 ile 120 arasında olmalıdır.");

            RuleFor(p => p.UpdateHeartTestDataDto.Trestbps)
                .InclusiveBetween(50, 250).WithMessage("Dinlenme kan basıncı mantıklı bir değer olmalıdır (50-250 mm Hg).");

            RuleFor(p => p.UpdateHeartTestDataDto.Chol)
                .InclusiveBetween(100, 600).WithMessage("Kolesterol değeri 100 ile 600 mg/dl arasında olmalıdır.");

            // 2. Klinik Veriler (Seçim Kutuları)
            RuleFor(p => p.UpdateHeartTestDataDto.Cp)
                .InclusiveBetween(0, 3).WithMessage("Geçersiz göğüs ağrısı tipi seçimi.");

            RuleFor(p => p.UpdateHeartTestDataDto.Thalach)
                .InclusiveBetween(60, 220).WithMessage("Maksimum kalp hızı 60 ile 220 arasında olmalıdır.");

            RuleFor(p => p.UpdateHeartTestDataDto.Oldpeak)
                .GreaterThanOrEqualTo(0).WithMessage("ST depresyonu negatif bir değer olamaz.")
                .LessThanOrEqualTo(10).WithMessage("ST depresyonu değeri çok yüksek.");

            RuleFor(p => p.UpdateHeartTestDataDto.Ca)
                .InclusiveBetween(0, 4).WithMessage("Damar sayısı 0 ile 4 arasında olmalıdır.");
        }
    }
}
