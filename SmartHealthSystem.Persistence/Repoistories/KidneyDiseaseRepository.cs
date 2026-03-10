using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Persistence.AppDbContext;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Persistence.Repoistories
{
    public class KidneyDiseaseRepository : GenericRepository<KidneyTestData>, IKidneyDiseaseRepository
    {
        private readonly AppDbContexts _context;

        public KidneyDiseaseRepository(AppDbContexts context) : base(context)
        {
            _context = context;
        }
        public async Task<IReadOnlyList<KidneyTestData>> GetAnalysesByPatientId(string patientId)
        {
            // String gelen PatientId'yi veritabanındaki Guid tipine dönüştürüyoruz
            var userGuid = Guid.Parse(patientId);

            return await _context.KidneyTestDatas
                .Include(x => x.TestResult) // İlişkili genel analiz sonuçlarını (Risk, HealthScore vb.) dahil ediyoruz
                .Where(x => x.TestResult.UserId == userGuid) // Filtreyi bağlı olduğu TestResult üzerinden yapıyoruz
                .OrderByDescending(x => x.CreatedDate) // En güncel analizi en üstte göstermek için tarih sıralaması
                .ToListAsync(); // Verileri liste olarak asenkron şekilde döndürüyoruz
        }
        // Eğer IKidneyDiseaseRepository içinde özel bir metod tanımlasaydın, 
        // onun kodlarını burada yazacaktık.
    }
}
