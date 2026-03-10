using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Contracts
{
    public interface IHeartDiseaseRepository : IGenericRepository<HeartTestData>
    {
        // Buraya kalp hastalığına özel ekstra metodlar eklenebilir
        // Örn: Bir hastaya ait tüm analizleri getir gibi
        Task<IReadOnlyList<HeartTestData>> GetAnalysesByPatientId(string patientId);
        Task<IReadOnlyList<HeartTestData>> GetAllAnalysesWithResults();
    }
}
