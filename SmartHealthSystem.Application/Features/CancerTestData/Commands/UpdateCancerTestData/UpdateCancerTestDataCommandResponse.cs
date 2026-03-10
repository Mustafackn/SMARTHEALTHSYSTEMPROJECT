using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.UpdateCancerTestData
{
    public class UpdateCancerTestDataCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public double NewRiskPercentage { get; set; }
    }
}
