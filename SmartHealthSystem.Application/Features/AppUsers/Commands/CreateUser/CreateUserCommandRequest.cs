using MediatR;
using SmartHealthSystem.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.AppUsers.Commands.CreateUser
{
    public class CreateUserCommandRequest : IRequest<CreateUserCommandResponse>
    {
        // Artık tek tek property yazmak yerine DTO'yu veriyoruz
        public CreateUserDto UserDto { get; set; }
    }
}
