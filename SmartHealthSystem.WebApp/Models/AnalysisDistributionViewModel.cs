namespace SmartHealthSystem.WebApp.Models
{
    public class AnalysisDistributionViewModel
    {
        public int HeartAnalysisCount { get; set; }
        public int KidneyAnalysisCount { get; set; }
        public int CancerAnalysisCount { get; set; }
        public int SocialActivityCount { get; set; } // Görseldeki "Sosyal" kısmı için

        // Yüzdelik hesaplama gerekirse yardımcı propertyler
        public double HeartPercentage => CalculatePercentage(HeartAnalysisCount);
        public double KidneyPercentage => CalculatePercentage(KidneyAnalysisCount);

        private double CalculatePercentage(int count)
        {
            int total = HeartAnalysisCount + KidneyAnalysisCount + CancerAnalysisCount + SocialActivityCount;
            return total == 0 ? 0 : Math.Round((double)count / total * 100, 1);
        }
    }
}
