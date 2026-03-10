using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.CreateHeartTestData
{
    public class CreateHeartTestDataCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public double RiskPercentage { get; set; }
        public int HealthScore { get; set; }
        public string Recommendations { get; set; }
    }
}
