using SmartHealthSystem.Domain.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Domain.Entites
{
    public class CancerTestData : BaseEntity
    {
        // TestResult tablosu ile ilişki (Böbrek ve Kalpteki gibi)
        public int TestResultId { get; set; }
        public virtual TestResult TestResult { get; set; } = null!;

        // Python modelinin beklediği 10 temel parametre
        // Hassasiyet için hepsini double tanımlıyoruz
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
    }
}
