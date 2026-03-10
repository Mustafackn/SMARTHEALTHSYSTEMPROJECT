using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.HeartTestData.Queries.GetAllHeartTestResults
{
    public class GetAllHeartTestDataQueryResponse
    {
        public int Id { get; set; }
        public int Age { get; set; }
        public int Sex { get; set; }
        public int Cp { get; set; }
        public int Trestbps { get; set; }
        public int Chol { get; set; }
        public int Fbs { get; set; }
        public int Restecg { get; set; }
        public int Thalach { get; set; }
        public int Exang { get; set; }
        public float Oldpeak { get; set; }
        public int Slope { get; set; }
        public int Ca { get; set; }
        public int Thal { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
