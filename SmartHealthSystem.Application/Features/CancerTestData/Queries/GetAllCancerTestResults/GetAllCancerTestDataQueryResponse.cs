using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Queries.GetAllCancerTestResults
{
    public class GetAllCancerTestDataQueryResponse
    {
        public int Id { get; set; }
        // float yerine double yaparak tip uyumsuzluğunu gideriyoruz
        public double RadiusMean { get; set; }
        public double TextureMean { get; set; }
        public double PerimeterMean { get; set; }
        public double AreaMean { get; set; }
        public double SmoothnessMean { get; set; }
        public double CompactnessMean { get; set; }
        public double ConcavityMean { get; set; }
        public double ConcavePointsMean { get; set; }
        public double SymmetryMean { get; set; }
        public double FractalDimensionMean { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
