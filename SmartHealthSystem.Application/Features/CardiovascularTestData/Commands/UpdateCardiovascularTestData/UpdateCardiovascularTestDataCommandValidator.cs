using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.UpdateCardiovascularTestData
{
    public class UpdateCardiovascularTestDataCommandValidator : AbstractValidator<UpdateCardiovascularTestDataCommandRequest>
    {
        public UpdateCardiovascularTestDataCommandValidator()
        {
            RuleFor(p => p.UpdateCardioDto.Id).GreaterThan(0).WithMessage("Geçerli bir analiz ID'si gereklidir.");
            RuleFor(p => p.UpdateCardioDto.Age).InclusiveBetween(1, 120);
            RuleFor(p => p.UpdateCardioDto.SystolicBP).InclusiveBetween(60, 250).WithMessage("Sistolik kan basıncı hatalı.");
            RuleFor(p => p.UpdateCardioDto.DiastolicBP).InclusiveBetween(40, 150).WithMessage("Diastolik kan basıncı hatalı.");
        }
    }
}
