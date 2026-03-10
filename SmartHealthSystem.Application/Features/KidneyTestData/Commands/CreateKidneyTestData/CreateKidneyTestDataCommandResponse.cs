using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.CreateKidneyTestData
{
    public class CreateKidneyTestDataCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }

        // Python API'den (v3) gelen analiz sonuçları
        public double RiskPercentage { get; set; } // % risk oranı
        public double HealthScore { get; set; }    // 100 üzerinden sağlık puanı
        public string Recommendations { get; set; } // Yapay zeka tavsiyesi

        // Opsiyonel: İşlem yapılan hasta ID'si
        public int PatientId { get; set; }
    }
}
