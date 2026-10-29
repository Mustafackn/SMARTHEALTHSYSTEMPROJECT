using SmartHealthSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Entites
{
    public class CardiovascularTestData : BaseEntity
    {
        // Merkezi TestResult tablosuna bağlantı (Foreign Key)
        public int TestResultId { get; set; }
        public virtual TestResult TestResult { get; set; } = null!;

        // --- Kardiyovasküler Teknik Parametreler (70k Dataset ile Uyumlu) ---

        public int Age { get; set; } // Yıl bazında yaş

        public int Gender { get; set; } // 1: Kadın, 2: Erkek

        public int Height { get; set; } // cm cinsinden

        public float Weight { get; set; } // kg cinsinden

        public int SystolicBP { get; set; } // ap_hi (Büyük Tansiyon)

        public int DiastolicBP { get; set; } // ap_lo (Küçük Tansiyon)

        /// <summary>
        /// 1: Normal, 2: Üst Sınır, 3: Yüksek
        /// </summary>
        public int Cholesterol { get; set; }

        /// <summary>
        /// 1: Normal, 2: Üst Sınır, 3: Yüksek
        /// </summary>
        public int Glucose { get; set; }

        public bool IsSmoking { get; set; } // 0 veya 1

        public bool IsAlcoholConsumer { get; set; } // 0 veya 1

        public bool IsActive { get; set; } // Fiziksel aktivite (0 veya 1)
    }
}
