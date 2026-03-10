using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Contracts
{
    public interface ICancerTestDataRepository : IGenericRepository<CancerTestData>
    {

        Task<IReadOnlyList<CancerTestData>> GetAllAnalysesWithResults();
        Task<IReadOnlyList<CancerTestData>> GetAnalysesByPatientId(string patientId);
    }
}
