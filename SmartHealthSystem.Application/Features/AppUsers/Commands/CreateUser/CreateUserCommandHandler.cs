using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.AppUsers.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommandRequest, CreateUserCommandResponse>
    {
        private readonly UserManager<AppUser> _userManager;

        public CreateUserCommandHandler(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        public async Task<CreateUserCommandResponse> Handle(CreateUserCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Kullanıcı nesnesini hazırlıyoruz
            var user = new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.UserDto.FirstName,
                LastName = request.UserDto.LastName,
                Email = request.UserDto.Email,
                UserName = request.UserDto.UserName
            };

            // 2. Kullanıcıyı veritabanına kaydediyoruz
            var result = await _userManager.CreateAsync(user, request.UserDto.Password);

            if (result.Succeeded)
            {
                // 3. Kayıt başarılıysa, kullanıcıya otomatik olarak "User" rolünü tanımlıyoruz
                // Bu isim, SeedData içinde verdiğin "User" ismiyle tam eşleşmelidir.
                await _userManager.AddToRoleAsync(user, "User");

                return new CreateUserCommandResponse
                {
                    Succeeded = true,
                    Message = "Kullanıcı başarıyla oluşturuldu ve 'User' rolü atandı."
                };
            }

            // Hata durumunda mesajları birleştirip dönüyoruz
            return new CreateUserCommandResponse
            {
                Succeeded = false,
                Message = string.Join(", ", result.Errors.Select(x => x.Description))
            };
        }
    }
}
