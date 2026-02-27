using MediatR;
using SmartHealthSystem.Application.DTOs.User;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.LoginUser
{
    public class LoginUserCommandRequest : IRequest<LoginUserCommandResponse>
    {
        // Doğrudan alanlar yerine DTO nesnesini alıyoruz
        public LoginDto LoginDto { get; set; }
    }
}
