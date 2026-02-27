using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.AppUsers.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommandRequest>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(x => x.UserDto.Email)
                .NotEmpty().WithMessage("E-posta adresi boş geçilemez.")
                .EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz.");

            RuleFor(x => x.UserDto.FirstName)
                .NotEmpty().WithMessage("İsim alanı boş geçilemez.")
                .MaximumLength(50).WithMessage("İsim en fazla 50 karakter olabilir.");

            RuleFor(x => x.UserDto.LastName)
                .NotEmpty().WithMessage("Soyisim alanı boş geçilemez.");

            RuleFor(x => x.UserDto.UserName)
                .NotEmpty().WithMessage("Kullanıcı adı boş geçilemez.")
                .MinimumLength(3).WithMessage("Kullanıcı adı en az 3 karakter olmalıdır.");

            RuleFor(x => x.UserDto.Password)
                .NotEmpty().WithMessage("Şifre boş geçilemez.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(x => x.UserDto.ConfirmPassword)
                .Equal(x => x.UserDto.Password).WithMessage("Şifreler birbiriyle uyuşmuyor.");
        }
    }
}
