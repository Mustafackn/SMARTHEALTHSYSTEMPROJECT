using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.HeratTestData
{
    public class UpdateHeartTestDataDto : BaseHeartTestDataDto
    {
        public string Id { get; set; } // Güncellenecek kaydı bulmak için ID şart
        public string PatientId { get; set; }
    }
}
