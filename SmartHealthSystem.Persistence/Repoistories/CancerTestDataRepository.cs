using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Persistence.AppDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence.Repoistories
{
    public class CancerTestDataRepository : GenericRepository<CancerTestData>, ICancerTestDataRepository
    {
        public CancerTestDataRepository(AppDbContexts context) : base(context)
        {
        }
        public async Task<IReadOnlyList<CancerTestData>> GetAnalysesByPatientId(string patientId)
        {
            // Guid dönüşümü: Senin TestResult sınıfında UserId Guid tipindeyse
            var userGuid = Guid.Parse(patientId);

            return await _context.CancerTestDatas
                .Include(x => x.TestResult) // Önce ilişkili TestResult tablosunu bağlıyoruz
                .Where(x => x.TestResult.UserId == userGuid) // Filtreyi bağlı olduğu TestResult üzerinden yapıyoruz
                .ToListAsync();
        }
        public async Task<IReadOnlyList<CancerTestData>> GetAllAnalysesWithResults()
        {
            return await _context.CancerTestDatas
                .Include(x => x.TestResult) // Teknik verinin yanına analiz sonucunu (Risk vb.) bağlar
                .ToListAsync();
        }
    }
}
