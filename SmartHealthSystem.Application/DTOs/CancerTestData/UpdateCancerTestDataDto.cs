using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.CancerTestData
{
    public class UpdateCancerTestDataDto : BaseCancerTestDataDto
    {
        public int Id { get; set; } // Güncellenecek kaydın ID'si
        public string PatientId { get; set; }
    }
}
