using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.DiabetesTestData
{
    public class DiabetesTestDataDto : BaseDiabetesTestDataDto
    {
        public int Id { get; set; } // Veritabanı ID'si
        public string PatientId { get; set; } = null!;
        public int Result { get; set; } // AI Tahmin Sonucu (0 veya 1)
        public DateTime CreatedDate { get; set; }
    }
}
