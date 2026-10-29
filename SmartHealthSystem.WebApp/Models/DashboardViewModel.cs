namespace SmartHealthSystem.WebApp.Models
{
    public class DashboardViewModel
    {
        // Üstteki İstatistik Kartları
        public int TotalPatientCount { get; set; }
        public int TotalAnalysisCount { get; set; }
        public int PendingResultsCount { get; set; }

        // Görseldeki "Son Yazışmalar" kısmı için (Son 5-10 Analiz)
        public List<RecentAnalysisViewModel> RecentAnalyses { get; set; }

        // Görseldeki "Kategori Dağılımı" (Sağ taraf)
        public AnalysisDistributionViewModel Distribution { get; set; }
    }
}
