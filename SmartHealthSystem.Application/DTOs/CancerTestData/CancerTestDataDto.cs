using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.CancerTestData
{
    public class CancerTestDataDto : BaseCancerTestDataDto
    {
        public int Id { get; set; } // Veritabanı ID'si (int)
        public string PatientId { get; set; }
        public bool IsAtRisk { get; set; } // %95.61 başarıyla tahmin edilen risk durumu
        public double RiskPercentage { get; set; } // %75.5 gibi yüzdelik oran
        public DateTime CreatedDate { get; set; }
    }
}
