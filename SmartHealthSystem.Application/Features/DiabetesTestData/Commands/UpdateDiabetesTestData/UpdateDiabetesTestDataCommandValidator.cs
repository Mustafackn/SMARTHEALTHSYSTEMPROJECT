using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.UpdateDiabetesTestData
{
    public class UpdateDiabetesTestDataCommandValidator : AbstractValidator<UpdateDiabetesTestDataCommandRequest>
    {
        public UpdateDiabetesTestDataCommandValidator()
        {
            // Kayıt Bulma Kontrolü
            RuleFor(p => p.UpdateDiabetesTestDataDto.Id)
                .GreaterThan(0).WithMessage("Geçerli bir analiz ID'si girilmelidir.");

            // Yaş Doğrulaması
            RuleFor(p => p.UpdateDiabetesTestDataDto.Age)
                .InclusiveBetween(1f, 120f).WithMessage("Yaş 1 ile 120 arasında olmalıdır.");

            // BMI Doğrulaması
            RuleFor(p => p.UpdateDiabetesTestDataDto.BMI)
                .InclusiveBetween(10f, 60f).WithMessage("BMI değeri mantıklı sınırlar dışında (10-60).");

            // Şeker Seviyeleri Doğrulaması
            RuleFor(p => p.UpdateDiabetesTestDataDto.HbA1cLevel)
                .InclusiveBetween(3f, 15f).WithMessage("HbA1c seviyesi 3 ile 15 arasında olmalıdır.");

            RuleFor(p => p.UpdateDiabetesTestDataDto.BloodGlucoseLevel)
                .InclusiveBetween(50f, 500f).WithMessage("Kan şekeri değeri 50-500 arasında olmalıdır.");

            // Boş Geçilemez Alanlar
            RuleFor(p => p.UpdateDiabetesTestDataDto.Gender)
                .NotEmpty().WithMessage("Cinsiyet alanı boş bırakılamaz.");

            RuleFor(p => p.UpdateDiabetesTestDataDto.SmokingHistory)
                .NotEmpty().WithMessage("Sigara geçmişi bilgisi zorunludur.");
        }
    }
}
