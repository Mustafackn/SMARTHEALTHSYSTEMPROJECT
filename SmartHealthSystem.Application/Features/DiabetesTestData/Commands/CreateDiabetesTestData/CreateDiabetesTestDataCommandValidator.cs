using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.CreateDiabetesTestData
{
    public class CreateDiabetesTestDataCommandValidator : AbstractValidator<CreateDiabetesTestDataCommandRequest>
    {
        public CreateDiabetesTestDataCommandValidator()
        {
            RuleFor(p => p.Age)
                .InclusiveBetween(1f, 120f).WithMessage("Yaş 1 ile 120 arasında olmalıdır.");

            RuleFor(p => p.BMI)
                .InclusiveBetween(10f, 70f).WithMessage("Vücut kitle indeksi (BMI) mantıklı bir değer olmalıdır.");

            RuleFor(p => p.HbA1cLevel)
                .InclusiveBetween(3f, 15f).WithMessage("HbA1c seviyesi 3 ile 15 arasında olmalıdır.");

            RuleFor(p => p.BloodGlucoseLevel)
                .InclusiveBetween(50f, 500f).WithMessage("Kan şekeri seviyesi 50 ile 500 arasında olmalıdır.");

            RuleFor(p => p.PatientId)
                .NotEmpty().WithMessage("Hasta bilgisi gereklidir.");
        }
    }
}
