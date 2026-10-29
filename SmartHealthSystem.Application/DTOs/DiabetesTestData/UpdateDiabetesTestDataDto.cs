using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.DiabetesTestData
{
    public class UpdateDiabetesTestDataDto : BaseDiabetesTestDataDto
    {
        public int Id { get; set; } // Güncellenecek kaydı bulmak için
        public string PatientId { get; set; } = null!;
    }
}
