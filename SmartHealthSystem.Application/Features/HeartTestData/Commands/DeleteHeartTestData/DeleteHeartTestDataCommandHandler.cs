using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.DeleteHeartTestData
{
    public class DeleteHeartTestDataCommandHandler : IRequestHandler<DeleteHeartTestDataCommandRequest, DeleteHeartTestDataCommandResponse>
    {
        private readonly IHeartDiseaseRepository _repository;
        private readonly IGenericRepository<SmartHealthSystem.Domain.Entites.TestResult> _testResultRepository;

        public DeleteHeartTestDataCommandHandler(IHeartDiseaseRepository repository, IGenericRepository<SmartHealthSystem.Domain.Entites.TestResult> testResultRepository)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
        }

        public async Task<DeleteHeartTestDataCommandResponse> Handle(DeleteHeartTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. ADIM: Id değerini açıkça değişkene alıp sorguluyoruz.
            // .AsNoTracking()'in Repository'den kalktığından %100 emin olmalısın!
            int targetId = request.Id;

            var dataToDelete = await _repository.GetWhere(x => x.Id == targetId)
                                                .Include(x => x.TestResult)
                                                .FirstOrDefaultAsync(cancellationToken);

            // 2. ADIM: Kayıt hala bulunamıyorsa manuel bir kontrol yapıyoruz
            if (dataToDelete == null)
            {
                // Alternatif olarak doğrudan GetById üzerinden deneme yapabilirsin
                dataToDelete = await _repository.GetByIdAsync(targetId);

                if (dataToDelete == null)
                {
                    return new DeleteHeartTestDataCommandResponse
                    {
                        IsSuccess = false,
                        Message = $"Hata: Veritabanında {targetId} ID'li bir kayıt fiziksel olarak mevcut değil."
                    };
                }
            }

            try
            {
                // 3. ADIM: Önce bağımlı tabloyu (TestResult) sil
                if (dataToDelete.TestResult != null)
                {
                    await _testResultRepository.DeleteAsync(dataToDelete.TestResult);
                }

                // 4. ADIM: Ana tabloyu (HeartTestData) sil
                await _repository.DeleteAsync(dataToDelete);

                return new DeleteHeartTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = "Analiz verisi ve bağlı sonuçlar başarıyla silindi."
                };
            }
            catch (Exception ex)
            {
                return new DeleteHeartTestDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Silme işlemi sırasında teknik hata: {ex.Message}"
                };
            }
        }
    }
}