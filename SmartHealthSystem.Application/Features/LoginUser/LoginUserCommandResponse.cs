using SmartHealthSystem.Application.DTOs.Tokens;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.LoginUser
{
    public class LoginUserCommandResponse
    {
        public Token Token { get; set; }
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
