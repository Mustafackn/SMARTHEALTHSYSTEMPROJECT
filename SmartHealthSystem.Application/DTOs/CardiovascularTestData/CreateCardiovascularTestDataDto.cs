using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.CardiovascularTestData
{
    public class CreateCardiovascularTestDataDto : BaseCardiovascularTestDataDto
    {
        public Guid UserId { get; set; } // Testi yapan kullanıcının ID'si
    }
}
