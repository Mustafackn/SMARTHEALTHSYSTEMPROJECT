using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.CardiovascularTestData
{
    public abstract class BaseCardiovascularTestDataDto
    {
        public int Age { get; set; }
        public int Gender { get; set; } // 1: Kadın, 2: Erkek
        public int Height { get; set; } // cm
        public float Weight { get; set; } // kg
        public int SystolicBP { get; set; } // ap_hi
        public int DiastolicBP { get; set; } // ap_lo
        public int Cholesterol { get; set; } // 1, 2, 3
        public int Glucose { get; set; } // 1, 2, 3
        public int Smoke { get; set; } // 0 veya 1
        public int Alco { get; set; } // 0 veya 1
        public int Active { get; set; } // 0 veya 1
    }
}
