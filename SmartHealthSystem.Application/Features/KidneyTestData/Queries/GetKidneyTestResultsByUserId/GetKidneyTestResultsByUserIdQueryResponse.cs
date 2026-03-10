using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Queries.GetKidneyTestResultsByUserId
{
    public class GetKidneyTestResultsByUserIdQueryResponse
    {
        public int Id { get; set; }
        public string UserId { get; set; } // Kullanıcı ID'si eklendi
        public int Age { get; set; }
        public int Bp { get; set; }        // Kan Basıncı
        public float Sg { get; set; }      // Özgül Ağırlık
        public int Al { get; set; }        // Albümin
        public int Su { get; set; }        // Şeker
        public int Bgr { get; set; }       // Kan Şekeri
        public int Bu { get; set; }        // Kan Üresi
        public float Sc { get; set; }      // Serum Kreatinin
        public int Sod { get; set; }       // Sodyum
        public float Pot { get; set; }     // Potasyum
        public float Hemo { get; set; }    // Hemoglobin
        public DateTime CreatedDate { get; set; }
    }
}
