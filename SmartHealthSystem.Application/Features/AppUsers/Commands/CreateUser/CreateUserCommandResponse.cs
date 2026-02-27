using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.AppUsers.Commands.CreateUser
{
    public class CreateUserCommandResponse
    {
        public bool Succeeded { get; set; }
        public string Message { get; set; }
    }
}
