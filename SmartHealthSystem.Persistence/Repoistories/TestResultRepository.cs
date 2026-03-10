using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Persistence.AppDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence.Repoistories
{
    public class TestResultRepository : GenericRepository<TestResult>, ITestResultRepository
    {
        public TestResultRepository(AppDbContexts context) : base(context)
        {
        }

        public async Task<List<TestResult>> GetTestResultsWithTypesByUserIdAsync(Guid userId)
        {
            return await _context.TestResults
                .Include(tr => tr.TestType) // TestTypes tablosunu dahil et
                .Where(tr => tr.UserId == userId) // Sadece bu hastanın verileri
                .OrderByDescending(tr => tr.CreatedDate) // En yeni test en üstte
                .ToListAsync();
        }

        // 2. Güncelleme işlemi için tek bir testi detaylarıyla (TestType) getirir
        public async Task<TestResult> GetTestResultWithDetailsByIdAsync(int id)
        {
            return await _context.TestResults
                .Include(tr => tr.TestType) // Testin hangi türde olduğunu formda göstermek için
                .FirstOrDefaultAsync(tr => tr.Id == id); // int Id ile sorgula
        }
        public async Task<CancerTestData> GetCancerDataByTestResultIdAsync(int testResultId)
        {
            return await _context.CancerTestDatas
                .FirstOrDefaultAsync(x => x.TestResultId == testResultId);
        }

        public async Task<HeartTestData> GetHeartDataByTestResultIdAsync(int testResultId)
        {
            return await _context.HeartTestDatas
                .FirstOrDefaultAsync(x => x.TestResultId == testResultId);
        }

        public async Task<KidneyTestData> GetKidneyDataByTestResultIdAsync(int testResultId)
        {
            // Veritabanındaki KidneyTestDatas tablosuna gidip TestResultId ile eşleşen ilk kaydı bulur
            return await _context.KidneyTestDatas
                .FirstOrDefaultAsync(x => x.TestResultId == testResultId);
        }
    }
}
