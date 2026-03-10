using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.UpdateKidneyTestData
{
    public class UpdateKidneyTestDataCommandValidator : AbstractValidator<UpdateKidneyTestDataCommandRequest>
    {
        public UpdateKidneyTestDataCommandValidator()
        {
            // ID ve Hasta Kontrolü
            RuleFor(p => p.UpdateKidneyTestDataDto.Id)
                .NotEmpty().WithMessage("Güncellenecek analiz ID'si boş olamaz.");

            RuleFor(p => p.UpdateKidneyTestDataDto.PatientId)
     .NotEmpty().WithMessage("Analizin hangi hastaya ait olduğu belirtilmelidir.");

            // Laboratuvar Verileri Doğrulaması
            RuleFor(p => p.UpdateKidneyTestDataDto.Age)
                .InclusiveBetween(1, 120).WithMessage("Yaş 1-120 arasında olmalıdır.");

            RuleFor(p => p.UpdateKidneyTestDataDto.Bp)
                .InclusiveBetween(50, 200).WithMessage("Kan basıncı (BP) mantıklı bir değer olmalıdır.");

            RuleFor(p => p.UpdateKidneyTestDataDto.Sg)
                .InclusiveBetween(1.000, 1.050).WithMessage("Özgül ağırlık (SG) 1.000-1.050 arasında olmalıdır.");

            RuleFor(p => p.UpdateKidneyTestDataDto.Al)
                .InclusiveBetween(0, 5).WithMessage("Albümin (AL) 0-5 arasında olmalıdır.");

            RuleFor(p => p.UpdateKidneyTestDataDto.Sc)
                .GreaterThan(0).WithMessage("Serum Kreatinin (SC) 0'dan büyük olmalıdır.");

            RuleFor(p => p.UpdateKidneyTestDataDto.Hemo)
                .InclusiveBetween(3.0, 20.0).WithMessage("Hemoglobin (HEMO) değeri 3-20 arasında olmalıdır.");
        }
    }
}
