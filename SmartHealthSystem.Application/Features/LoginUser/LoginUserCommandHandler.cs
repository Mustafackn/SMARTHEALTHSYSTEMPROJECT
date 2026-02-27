using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartHealthSystem.Application.Abstractions.Tokenss;
using SmartHealthSystem.Application.DTOs.Tokens;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.LoginUser
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommandRequest, LoginUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        private readonly ITokenHandler _tokenHandler;

        public LoginUserCommandHandler(
            UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager,
            ITokenHandler tokenHandler)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenHandler = tokenHandler;
        }

        public async Task<LoginUserCommandResponse> Handle(LoginUserCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Kullanıcıyı bulma (Email veya UserName ile)
            AppUser user = await _userManager.FindByEmailAsync(request.LoginDto.UserNameOrEmail);
            if (user == null)
                user = await _userManager.FindByNameAsync(request.LoginDto.UserNameOrEmail);

            if (user == null)
            {
                return new() { Succeeded = false, Message = "Kullanıcı adı veya şifre hatalı!" };
            }

            // 2. Şifre kontrolü (Lockout on failure: false -> Çok kez yanlış girerse hesabı kilitlemiyoruz şimdilik)
            SignInResult result = await _signInManager.CheckPasswordSignInAsync(user, request.LoginDto.Password, false);

            if (result.Succeeded)
            {
                // 3. Giriş başarılıysa 15 dakikalık JWT üret
                Token token = _tokenHandler.CreateAccessToken(15, user);

                return new()
                {
                    Token = token,
                    Succeeded = true,
                    Message = "Giriş başarıyla gerçekleştirildi."
                };
            }

            // 4. Şifre yanlışsa veya hesap kilitliyse
            return new()
            {
                Succeeded = false,
                Message = result.IsLockedOut ? "Çok fazla hatalı deneme! Hesabınız geçici olarak kilitlendi." : "Kullanıcı adı veya şifre hatalı!"
            };
        }
    }
}
