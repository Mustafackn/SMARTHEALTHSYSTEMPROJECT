using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Contracts
{
    public interface IDiabetesRepository : IGenericRepository<DiabetesTestData>
    {
        // Diyabet analizine özel ekstra metodlar

        // Belirli bir hastaya ait geçmiş tüm diyabet test verilerini getirir
        Task<IReadOnlyList<DiabetesTestData>> GetDiabetesAnalysesByPatientId(string patientId);

        // Tüm diyabet testlerini, bağlı oldukları TestResult (Risk analizi ve öneriler) ile birlikte getirir
        Task<IReadOnlyList<DiabetesTestData>> GetAllDiabetesAnalysesWithResults();
    }
}
