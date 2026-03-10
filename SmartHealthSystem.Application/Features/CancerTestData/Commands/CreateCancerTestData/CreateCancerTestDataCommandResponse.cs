using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.CreateCancerTestData
{
    public class CreateCancerTestDataCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public double RiskPercentage { get; set; }
        public double HealthScore { get; set; }
        public string Recommendations { get; set; }
    }
}
