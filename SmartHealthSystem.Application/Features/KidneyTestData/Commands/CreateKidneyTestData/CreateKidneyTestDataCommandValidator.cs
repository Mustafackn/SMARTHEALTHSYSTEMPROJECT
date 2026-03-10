using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.CreateKidneyTestData
{
    public class CreateKidneyTestDataCommandValidator : AbstractValidator<CreateKidneyTestDataCommandRequest>
    {
        public CreateKidneyTestDataCommandValidator()
        {
            // 1. Temel Demografik ve Vital Veriler
            RuleFor(p => p.Age)
                .NotEmpty().WithMessage("Yaş alanı boş geçilemez.")
                .InclusiveBetween(1, 120).WithMessage("Yaş 1 ile 120 arasında olmalıdır.");

            RuleFor(p => p.Bp)
                .InclusiveBetween(50, 200).WithMessage("Kan basıncı (BP) 50 ile 200 mmHg arasında olmalıdır.");

            // 2. İdrar Tahlili Parametreleri (Kritik Değerler)
            RuleFor(p => p.Sg)
                .InclusiveBetween(1.005, 1.030).WithMessage("Özgül ağırlık (SG) 1.005 ile 1.030 arasında olmalıdır.");

            RuleFor(p => p.Al)
                .InclusiveBetween(0, 5).WithMessage("Albümin (AL) seviyesi 0 ile 5 arasında olmalıdır.");

            RuleFor(p => p.Su)
                .InclusiveBetween(0, 5).WithMessage("Şeker (SU) seviyesi 0 ile 5 arasında olmalıdır.");

            // 3. Laboratuvar Kan Verileri
            RuleFor(p => p.Bgr)
                .InclusiveBetween(50, 500).WithMessage("Kan şekeri (BGR) 50 ile 500 mg/dL arasında olmalıdır.");

            RuleFor(p => p.Bu)
                .InclusiveBetween(5, 400).WithMessage("Kan üresi (BU) 5 ile 400 mg/dL arasında olmalıdır.");

            RuleFor(p => p.Sc)
                .InclusiveBetween(0.1, 80).WithMessage("Serum kreatinin (SC) 0.1 ile 80 mg/dL arasında olmalıdır.");

            RuleFor(p => p.Hemo)
                .InclusiveBetween(3, 20).WithMessage("Hemoglobin (Hemo) 3 ile 20 g/dL arasında olmalıdır.");

            // 4. Seçim Kutuları (0: Normal, 1: Anormal Mantığı)
            RuleFor(p => p.Rbc)
                .InclusiveBetween(0, 1).WithMessage("Alyuvar (RBC) seçimi geçersiz.");

            RuleFor(p => p.Pc)
                .InclusiveBetween(0, 1).WithMessage("İltihap hücresi (PC) seçimi geçersiz.");

            // 5. İlişkisel Veriler
            RuleFor(p => p.PatientId)
                .NotEmpty().WithMessage("Bir hasta seçilmelidir.");
        }
    }
}
