using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Persistence.AppDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence.Repoistories
{
    public class DiabetesRepository : GenericRepository<DiabetesTestData>, IDiabetesRepository
    {
        private readonly AppDbContexts _dbContext;

        public DiabetesRepository(AppDbContexts dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<DiabetesTestData>> GetAllDiabetesAnalysesWithResults()
        {
            // Diyabet verilerini, bağlı oldukları TestResult (Öneriler ve Skorlar) ile birlikte getirir
            return await _dbContext.DiabetesTestDatas
                .Include(x => x.TestResult)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<DiabetesTestData>> GetDiabetesAnalysesByPatientId(string patientId)
        {
            // Belirli bir hastaya ait (UserId üzerinden) diyabet testlerini filtreler
            return await _dbContext.DiabetesTestDatas
                .Include(x => x.TestResult)
                .Where(x => x.TestResult.UserId.ToString() == patientId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
}
