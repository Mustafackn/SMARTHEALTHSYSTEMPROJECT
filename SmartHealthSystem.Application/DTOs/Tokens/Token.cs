using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.Tokens
{
    public class Token
    {
        public string AccessToken { get; set; }
        public DateTime Expiration { get; set; }
    }
}
