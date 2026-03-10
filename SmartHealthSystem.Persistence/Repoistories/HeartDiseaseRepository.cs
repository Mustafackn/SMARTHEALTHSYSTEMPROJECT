using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Persistence.AppDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence.Repoistories
{
    public class HeartDiseaseRepository : GenericRepository<HeartTestData>, IHeartDiseaseRepository
    {
        public HeartDiseaseRepository(AppDbContexts context) : base(context)
        {
        }

        public async Task<IReadOnlyList<HeartTestData>> GetAnalysesByPatientId(string patientId)
        {
            // Guid dönüşümü: Senin TestResult sınıfında UserId Guid tipindeydi.
            var userGuid = Guid.Parse(patientId);

            return await _context.HeartTestDatas
                .Include(x => x.TestResult) // Önce ilişkili TestResult tablosunu bağlıyoruz
                .Where(x => x.TestResult.UserId == userGuid) // Filtreyi bağlı olduğu TestResult üzerinden yapıyoruz
                .ToListAsync();
        }
        public async Task<IReadOnlyList<HeartTestData>> GetAllAnalysesWithResults()
        {
            return await _context.HeartTestDatas
                .Include(x => x.TestResult) // Teknik verinin yanına analiz sonucunu (Risk vb.) bağlar
                .OrderByDescending(x => x.CreatedDate) // En yeni analiz en üstte
                .ToListAsync();
        }
    }
}
