using SmartHealthSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Entites
{
    public class KidneyTestData : BaseEntity
    {
        public int TestResultId { get; set; }
        public virtual TestResult TestResult { get; set; } = null!;

        // --- Teknik Laboratuvar Parametreleri ---

        public double Age { get; set; }        // Yaş
        public double Bp { get; set; }         // Kan Basıncı (Blood Pressure)
        public double Sg { get; set; }         // İdrar Özgül Ağırlığı (Specific Gravity)
        public double Al { get; set; }         // Albümin
        public double Su { get; set; }         // Şeker (Sugar)

        // Kategorik Değerler (0: Normal, 1: Anormal/Var)
        public int Rbc { get; set; }           // Kırmızı Kan Hücreleri
        public int Pc { get; set; }            // İltihap Hücresi (Pus Cell)
        public int Pcc { get; set; }           // İltihap Hücresi Kümeleri
        public int Ba { get; set; }            // Bakteri

        // Kan Analiz Değerleri
        public double Bgr { get; set; }        // Kan Glukoz (Rastgele)
        public double Bu { get; set; }         // Kan Üresi
        public double Sc { get; set; }         // Serum Kreatinin
        public double Sod { get; set; }        // Sodyum
        public double Pot { get; set; }        // Potasyum
        public double Hemo { get; set; }       // Hemoglobin
    }
}
