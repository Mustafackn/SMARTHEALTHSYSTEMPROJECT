using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.UpdateKidneyTestData
{
    public class UpdateKidneyTestDataCommandResponse
    {
        public bool IsSuccess { get; set; }
        public string Message { get; set; }
        public double NewRiskPercentage { get; set; } // Güncelleme sonrası yeni Böbrek risk sonucu
    }
}
