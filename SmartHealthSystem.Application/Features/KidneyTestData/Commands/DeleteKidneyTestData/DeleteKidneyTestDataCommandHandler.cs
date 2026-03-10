using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.DeleteKidneyTestData
{
    public class DeleteKidneyTestDataCommandHandler : IRequestHandler<DeleteKidneyTestDataCommandRequest, DeleteKidneyTestDataCommandResponse>
    {
        private readonly IKidneyDiseaseRepository _repository;
        private readonly IGenericRepository<TestResult> _testResultRepository;

        public DeleteKidneyTestDataCommandHandler(IKidneyDiseaseRepository repository, IGenericRepository<TestResult> testResultRepository)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
        }

        public async Task<DeleteKidneyTestDataCommandResponse> Handle(DeleteKidneyTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            int targetId = request.Id;

            // 1. ADIM: Veriyi ve TestResult ilişkisini getir
            var dataToDelete = await _repository.GetWhere(x => x.Id == targetId)
                                                .Include(x => x.TestResult)
                                                .FirstOrDefaultAsync(cancellationToken);

            // 2. ADIM: Kayıt kontrolü
            if (dataToDelete == null)
            {
                return new DeleteKidneyTestDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Hata: {targetId} ID'li böbrek analizi bulunamadı."
                };
            }

            try
            {
                // 3. ADIM: Önce bağlı tabloyu (TestResult) silmeliyiz
                if (dataToDelete.TestResult != null)
                {
                    await _testResultRepository.DeleteAsync(dataToDelete.TestResult);
                }

                // 4. ADIM: Teknik tabloyu (KidneyTestData) sil
                await _repository.DeleteAsync(dataToDelete);

                return new DeleteKidneyTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = "Böbrek analizi ve ilişkili tüm sonuçlar başarıyla silindi."
                };
            }
            catch (Exception ex)
            {
                return new DeleteKidneyTestDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Silme hatası: {ex.Message}"
                };
            }
        }
    }
}
