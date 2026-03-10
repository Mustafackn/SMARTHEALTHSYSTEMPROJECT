using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.CancerTestData
{
    public class CreateCancerTestDataDto : BaseCancerTestDataDto
    {
        // Analizi hangi hasta için yapıyoruz?
        public string PatientId { get; set; }
    }
}
