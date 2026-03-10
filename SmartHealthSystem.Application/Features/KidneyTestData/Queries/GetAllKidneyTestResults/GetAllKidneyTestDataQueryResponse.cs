using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Queries.GetAllKidneyTestResults
{
    public class GetAllKidneyTestDataQueryResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Hangi hastaya ait olduğunu görmek için
        public int Age { get; set; }
        public int Bp { get; set; }        // Kan Basıncı (Blood Pressure)
        public float Sg { get; set; }      // Özgül Ağırlık (Specific Gravity)
        public int Al { get; set; }        // Albümin
        public int Su { get; set; }        // Şeker (Sugar)
        public int Bgr { get; set; }       // Kan Şekeri
        public int Bu { get; set; }        // Kan Üresi
        public float Sc { get; set; }      // Serum Kreatinin
        public int Sod { get; set; }       // Sodyum
        public float Pot { get; set; }     // Potasyum
        public float Hemo { get; set; }    // Hemoglobin
        public DateTime CreatedDate { get; set; }
    }
}
