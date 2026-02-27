using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.User
{
    public class LoginDto
    {
        public string UserNameOrEmail { get; set; }
        public string Password { get; set; }
    }
}
