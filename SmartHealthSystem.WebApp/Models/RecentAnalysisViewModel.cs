namespace SmartHealthSystem.WebApp.Models
{
    public class RecentAnalysisViewModel
    {
        public string PatientName { get; set; }
        public string AnalysisType { get; set; } // Kalp, Böbrek, Kanser vb.
        public string ResultStatus { get; set; } // Riskli, Normal, Beklemede
        public DateTime AnalysisDate { get; set; }
        public string PatientEmail { get; set; }
    }
}
