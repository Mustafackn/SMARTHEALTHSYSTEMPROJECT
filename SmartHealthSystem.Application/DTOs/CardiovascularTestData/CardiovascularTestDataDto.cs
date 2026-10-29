using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.CardiovascularTestData
{
    public class CardiovascularTestDataDto : BaseCardiovascularTestDataDto
    {
        public int Id { get; set; }
        public int TestResultId { get; set; }
        public double RiskProbability { get; set; } // Python'dan dönen olasılık
        public string RiskStatus { get; set; } = null!; // "Yüksek Risk" / "Düşük Risk"
        public DateTime CreatedDate { get; set; }
    }
}

