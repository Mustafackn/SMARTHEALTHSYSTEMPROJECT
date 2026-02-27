using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.User
{
    public class GetAllUsersDto
    {
        public string Id { get; set; } // Identity User Id
        public string FirstName { get; set; }
        public string LastName { get; set; }

        // Flutter'da "Ad Soyad" şeklinde kolayca göstermek için
        public string FullName => $"{FirstName} {LastName}";

        public string UserName { get; set; }
        public string Email { get; set; }

        // Eğer kullanıcıların profil fotoğrafı varsa buraya eklenebilir
    }
}
