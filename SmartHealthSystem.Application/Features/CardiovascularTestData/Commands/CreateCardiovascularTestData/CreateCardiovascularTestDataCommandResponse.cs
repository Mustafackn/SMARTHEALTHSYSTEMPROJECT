using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.CreateCardiovascularTestData
{
    public class CreateCardiovascularTestDataCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; } = string.Empty;
        public double RiskPercentage { get; set; }
        public int HealthScore { get; set; }
        public string Recommendations { get; set; } = string.Empty;
    }
}
