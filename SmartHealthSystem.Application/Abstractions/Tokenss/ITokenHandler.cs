using SmartHealthSystem.Application.DTOs.Tokens;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Abstractions.Tokenss
{
    public interface ITokenHandler
    {
        // Kullanıcıya kaç dakikalık bir token verileceğini ve hangi kullanıcı olduğunu alır
        Token CreateAccessToken(int minute, AppUser user);
    }
}
