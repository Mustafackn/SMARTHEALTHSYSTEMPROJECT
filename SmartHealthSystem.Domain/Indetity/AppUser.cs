using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Indetity
{
    public class AppUser : IdentityUser<Guid>
    {
        // IdentityUser içindeki standart alanlara (Email, Phone vb.) ek olarak:
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;

        // Token bazlı authentication (JWT) için gerekli Refresh Token alanları
        public string? RefreshToken { get; set; }
        public DateTime? RefreshTokenExpiryTime { get; set; }

        // Opsiyonel: Kullanıcının sistemdeki tam adını dönen yardımcı bir property
        public string FullName => $"{FirstName} {LastName}";
    }
}
