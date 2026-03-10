using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.UpdateHeartTestData
{
    public class UpdateHeartTestDataCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public double NewRiskPercentage { get; set; } // Güncelleme sonrası yeni AI sonucu
    }
}
