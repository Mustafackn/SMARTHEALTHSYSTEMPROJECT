using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.HeratTestData
{
    public class HeartTestDataDto : BaseHeartTestDataDto
    {
        public string Id { get; set; }
        public string PatientId { get; set; }
        public int Result { get; set; } // AI Tahmin Sonucu
        public DateTime CreatedDate { get; set; }
    }
}
