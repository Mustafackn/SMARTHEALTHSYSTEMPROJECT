using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.DTOs.KidneyTestData
{
    public class KidneyTestDataDto : BaseKidneyTestDataDto
    {
        public string Id { get; set; }
        public int PatientId { get; set; }

        // AI Tahmin Sonucu (0: Sağlıklı, 1: Hasta)
        public int Result { get; set; }

        // Kaydın oluşturulma tarihi
        public DateTime CreatedDate { get; set; }

        // Opsiyonel: Test sonucuna bağlı TestResultId bağlantısı
        public int TestResultId { get; set; }
    }
}
