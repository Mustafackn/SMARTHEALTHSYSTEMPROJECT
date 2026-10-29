using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.DiabetesTestData
{
    public class CreateDiabetesTestDataDto : BaseDiabetesTestDataDto
    {
        public string PatientId { get; set; } = null!;
    }
}
