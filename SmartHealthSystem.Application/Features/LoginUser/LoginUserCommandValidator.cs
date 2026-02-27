using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.LoginUser
{
    public class LoginUserCommandValidator : AbstractValidator<LoginUserCommandRequest>
    {
        public LoginUserCommandValidator()
        {
            RuleFor(x => x.LoginDto.UserNameOrEmail)
                .NotEmpty().WithMessage("Kullanıcı adı veya e-posta adresi boş geçilemez.")
                .MinimumLength(3).WithMessage("Geçerli bir kullanıcı adı veya e-posta giriniz.");

            RuleFor(x => x.LoginDto.Password)
                .NotEmpty().WithMessage("Şifre boş geçilemez.");
        }
    }
}
