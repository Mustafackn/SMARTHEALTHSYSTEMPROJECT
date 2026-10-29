using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.UpdateDiabetesTestData
{
    public class UpdateDiabetesTestDataCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public double RiskPercentage { get; set; } // Yeni hesaplanan risk
        public int HealthScore { get; set; }       // Yeni sağlık skoru
        public string Recommendations { get; set; } = string.Empty; // Yeni öneriler
    }
}
