using SmartHealthSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Entites
{
    public class DiabetesTestData : BaseEntity
    {
        // Merkezi TestResult tablosuna bağlantı (Foreign Key)
        public int TestResultId { get; set; }
        public virtual TestResult TestResult { get; set; } = null!;

        // --- Diyabet Teknik Parametreleri ---
        public string Gender { get; set; } = null!;
        public float Age { get; set; }
        public int Hypertension { get; set; }
        public int HeartDisease { get; set; }
        public string SmokingHistory { get; set; } = null!;
        public float BMI { get; set; }
        public float HbA1cLevel { get; set; }
        public float BloodGlucoseLevel { get; set; }
    }
}
