using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Contracts
{
    public interface ITestResultRepository : IGenericRepository<TestResult>
    {
        Task<List<TestResult>> GetTestResultsWithTypesByUserIdAsync(Guid userId);
        Task<TestResult> GetTestResultWithDetailsByIdAsync(int id);

        // --- Hastalığa Özel Teknik Veri Getirme Metotları ---

        // 1. Meme Kanseri teknik verilerini getir
        Task<CancerTestData> GetCancerDataByTestResultIdAsync(int testResultId);

        // 2. Kalp Analizi teknik verilerini getir
        Task<HeartTestData> GetHeartDataByTestResultIdAsync(int testResultId);

        // 3. Böbrek Analizi teknik verilerini getir
        Task<KidneyTestData> GetKidneyDataByTestResultIdAsync(int testResultId);
    }
}
