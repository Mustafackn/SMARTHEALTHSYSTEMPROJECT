using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.CreateHeartTestData
{
    public class CreateHeartTestDataCommandValidator : AbstractValidator<CreateHeartTestDataCommandRequest>
    {
        public CreateHeartTestDataCommandValidator()
        {
            // 1. Temel Demografik Veriler
            RuleFor(p => p.Age)
                .NotEmpty().WithMessage("Yaş alanı boş geçilemez.")
                .InclusiveBetween(1, 120).WithMessage("Yaş 1 ile 120 arasında olmalıdır.");

            RuleFor(p => p.Trestbps)
                .InclusiveBetween(50, 250).WithMessage("Dinlenme kan basıncı mantıklı bir değer olmalıdır (50-250 mm Hg).");

            RuleFor(p => p.Chol)
                .InclusiveBetween(100, 600).WithMessage("Kolesterol değeri 100 ile 600 mg/dl arasında olmalıdır.");

            // 2. Klinik Veriler (Seçim Kutuları)
            RuleFor(p => p.Cp)
                .InclusiveBetween(0, 3).WithMessage("Geçersiz göğüs ağrısı tipi seçimi.");

            RuleFor(p => p.Thalach)
                .InclusiveBetween(60, 220).WithMessage("Maksimum kalp hızı 60 ile 220 arasında olmalıdır.");

            RuleFor(p => p.Oldpeak)
                .GreaterThanOrEqualTo(0).WithMessage("ST depresyonu negatif bir değer olamaz.")
                .LessThanOrEqualTo(10).WithMessage("ST depresyonu değeri çok yüksek.");

            RuleFor(p => p.Ca)
                .InclusiveBetween(0, 4).WithMessage("Damar sayısı 0 ile 4 arasında olmalıdır.");

            // 3. İlişkisel Veriler
            RuleFor(p => p.PatientId)
                .NotEmpty().WithMessage("Analizin hangi hastaya ait olduğu belirtilmelidir.");
        }
    }
}
