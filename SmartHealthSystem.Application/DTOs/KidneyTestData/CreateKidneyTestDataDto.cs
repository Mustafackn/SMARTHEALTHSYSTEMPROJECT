using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.KidneyTestData
{
    public class CreateKidneyTestDataDto : BaseKidneyTestDataDto
    {
        // Python API'nin beklediği PatientId alanı
        public string PatientId { get; set; }
    }
}
