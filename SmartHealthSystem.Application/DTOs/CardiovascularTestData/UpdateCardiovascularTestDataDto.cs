using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.CardiovascularTestData
{
    public class UpdateCardiovascularTestDataDto : BaseCardiovascularTestDataDto
    {
        public int Id { get; set; }
        public Guid UserId { get; set; }
    }
}
