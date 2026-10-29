using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.CreateCardiovascularTestData
{
    public class CreateCardiovascularTestDataCommandValidator : AbstractValidator<CreateCardiovascularTestDataCommandRequest>
    {
        public CreateCardiovascularTestDataCommandValidator()
        {
            RuleFor(p => p.Age).InclusiveBetween(1, 120).WithMessage("Yaş 1 ile 120 arasında olmalıdır.");
            RuleFor(p => p.Height).InclusiveBetween(50, 250).WithMessage("Boy 50 ile 250 cm arasında olmalıdır.");
            RuleFor(p => p.Weight).InclusiveBetween(30f, 300f).WithMessage("Kilo mantıklı bir değer olmalıdır.");
            RuleFor(p => p.SystolicBP).InclusiveBetween(60, 250).WithMessage("Büyük tansiyon (ap_hi) 60-250 arası olmalıdır.");
            RuleFor(p => p.DiastolicBP).InclusiveBetween(40, 150).WithMessage("Küçük tansiyon (ap_lo) 40-150 arası olmalıdır.");
            RuleFor(p => p.UserId).NotEmpty().WithMessage("Kullanıcı bilgisi gereklidir.");
        }
    }
}
