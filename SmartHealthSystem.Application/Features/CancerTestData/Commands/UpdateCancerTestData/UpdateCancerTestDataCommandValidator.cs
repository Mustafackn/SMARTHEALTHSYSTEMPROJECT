using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.UpdateCancerTestData
{
    public class UpdateCancerTestDataCommandValidator : AbstractValidator<UpdateCancerTestDataCommandRequest>
    {
        public UpdateCancerTestDataCommandValidator()
        {
            RuleFor(p => p.UpdateCancerTestDataDto.Id).NotEmpty().WithMessage("ID boş olamaz.");

            // Alan (Area) için senin veri setindeki mantıklı sınırları ekledim
            RuleFor(p => p.UpdateCancerTestDataDto.AreaMean)
                .InclusiveBetween(100.0, 3000.0).WithMessage("Alan (Area) 100-3000 arasında olmalıdır.");

            RuleFor(p => p.UpdateCancerTestDataDto.RadiusMean)
                .InclusiveBetween(5.0, 40.0).WithMessage("Yarıçap (Radius) 5-40 arasında olmalıdır.");
        }
    }
}
