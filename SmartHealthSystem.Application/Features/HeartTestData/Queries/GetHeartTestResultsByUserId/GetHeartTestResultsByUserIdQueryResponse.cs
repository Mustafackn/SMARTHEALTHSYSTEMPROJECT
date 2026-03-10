using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Queries.GetHeartTestResultsByUserId
{
    public class GetHeartTestResultsByUserIdQueryResponse
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int Cp { get; set; }
        public int Trestbps { get; set; }
        public int Chol { get; set; }
        public float Oldpeak { get; set; }
        public int Ca { get; set; }
        public DateTime CreatedDate { get; set; } // HeartTestData içindeki tarih
    }
}
