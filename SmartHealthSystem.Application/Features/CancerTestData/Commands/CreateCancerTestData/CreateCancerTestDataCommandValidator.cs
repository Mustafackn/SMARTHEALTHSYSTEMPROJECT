using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.CreateCancerTestData
{
    public class CreateCancerTestDataCommandValidator : AbstractValidator<CreateCancerTestDataCommandRequest>
    {
        public CreateCancerTestDataCommandValidator()
        {
            RuleFor(p => p.PatientId).NotEmpty().WithMessage("Bir hasta seçilmelidir.");

            // Ortalama Yarıçap (Genelde 6 ile 30 arasındadır)
            RuleFor(p => p.RadiusMean).InclusiveBetween(5.0, 40.0).WithMessage("Yarıçap değeri mantıklı sınırlar dışında.");

            // Alan (Genelde 140 ile 2500 arasındadır)
            RuleFor(p => p.AreaMean).InclusiveBetween(100.0, 3000.0).WithMessage("Alan (Area) değeri geçersiz.");

            // Küçük ondalıklı değerler (Pürüzsüzlük, İçbükeylik vb. genelde 0 ile 1 arasıdır)
            RuleFor(p => p.SmoothnessMean).InclusiveBetween(0.01, 0.5).WithMessage("Pürüzsüzlük değeri geçersiz.");
            RuleFor(p => p.ConcavityMean).InclusiveBetween(0.0, 1.0).WithMessage("İçbükeylik değeri geçersiz.");
        }
    }
}
