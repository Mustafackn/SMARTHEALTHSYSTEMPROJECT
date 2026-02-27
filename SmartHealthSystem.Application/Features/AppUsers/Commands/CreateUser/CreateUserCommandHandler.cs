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
            var result = await _userManager.CreateAsync(new AppUser
            {
                Id = Guid.NewGuid(),
                FirstName = request.UserDto.FirstName,
                LastName = request.UserDto.LastName,
                Email = request.UserDto.Email,
                UserName = request.UserDto.UserName
            }, request.UserDto.Password);

            if (result.Succeeded)
            {
                return new CreateUserCommandResponse { Succeeded = true, Message = "Kullanıcı başarıyla oluşturuldu." };
            }

            return new CreateUserCommandResponse { Succeeded = false, Message = string.Join(", ", result.Errors.Select(x => x.Description)) };
        }
    }
}
