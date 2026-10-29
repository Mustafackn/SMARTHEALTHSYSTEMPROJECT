using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Persistence.AppDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence.Repoistories
{
    public class CardiovascularRepository : GenericRepository<CardiovascularTestData>, ICardiovascularRepository
    {
        private readonly AppDbContexts _dbContext;

        public CardiovascularRepository(AppDbContexts dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IReadOnlyList<CardiovascularTestData>> GetAllCardioAnalysesWithResults()
        {
            // Kardiyovasküler verileri, bağlı oldukları TestResult (Öneriler ve Skorlar) ile birlikte getirir
            return await _dbContext.Set<CardiovascularTestData>()
                .Include(x => x.TestResult)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }

        public async Task<IReadOnlyList<CardiovascularTestData>> GetCardioAnalysesByUserId(string userId)
        {
            // Belirli bir kullanıcıya ait kardiyovasküler testlerini filtreler
            return await _dbContext.Set<CardiovascularTestData>()
                .Include(x => x.TestResult)
                .Where(x => x.TestResult.UserId.ToString() == userId)
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
        }
    }
}
