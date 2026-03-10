using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.KidneyTestData
{
    public class UpdateKidneyTestDataDto : BaseKidneyTestDataDto
    {
        public string Id { get; set; } // Güncellenecek kaydı bulmak için ID şart
        public string PatientId { get; set; } // Veri setindeki hasta ID'si
    }
}
