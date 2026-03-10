using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Contracts
{
    public interface IKidneyDiseaseRepository : IGenericRepository<KidneyTestData>
    {
        // Böbrek testine özel spesifik metodlar gerekirse buraya eklenebilir.
        Task<IReadOnlyList<KidneyTestData>> GetAnalysesByPatientId(string patientId);
    }
}
