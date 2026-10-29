using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Contracts
{
    public interface ICardiovascularRepository : IGenericRepository<CardiovascularTestData>
    {
        // Belirli bir kullanıcıya ait geçmiş tüm kardiyovasküler test verilerini getirir
        Task<IReadOnlyList<CardiovascularTestData>> GetCardioAnalysesByUserId(string userId);

        // Tüm kardiyovasküler testleri, bağlı oldukları TestResult (Risk skoru ve öneriler) ile birlikte getirir
        Task<IReadOnlyList<CardiovascularTestData>> GetAllCardioAnalysesWithResults();
    }
}
