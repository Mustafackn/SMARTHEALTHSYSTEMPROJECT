using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.DeleteCancerTestData
{
    public class DeleteCancerTestDataCommandHandler : IRequestHandler<DeleteCancerTestDataCommandRequest, DeleteCancerTestDataCommandResponse>
    {
        private readonly ICancerTestDataRepository _repository;
        private readonly IGenericRepository<TestResult> _testResultRepository;

        public DeleteCancerTestDataCommandHandler(ICancerTestDataRepository repository, IGenericRepository<TestResult> testResultRepository)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
        }

        public async Task<DeleteCancerTestDataCommandResponse> Handle(DeleteCancerTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            int targetId = request.Id;

            // 1. ADIM: Kanser verisini ve ilişkili TestResult sonucunu getir
            var dataToDelete = await _repository.GetWhere(x => x.Id == targetId)
                                                .Include(x => x.TestResult)
                                                .FirstOrDefaultAsync(cancellationToken);

            // 2. ADIM: Kayıt kontrolü
            if (dataToDelete == null)
            {
                return new DeleteCancerTestDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Hata: {targetId} ID'li kanser analizi bulunamadı."
                };
            }

            try
            {
                // 3. ADIM: Önce bağlı tabloyu (TestResult) sil (Foreign Key kısıtlaması nedeniyle)
                if (dataToDelete.TestResult != null)
                {
                    await _testResultRepository.DeleteAsync(dataToDelete.TestResult);
                }

                // 4. ADIM: Teknik tabloyu (CancerTestData) sil
                await _repository.DeleteAsync(dataToDelete);

                return new DeleteCancerTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = "Kanser analizi ve ilişkili tüm sonuçlar başarıyla silindi."
                };
            }
            catch (Exception ex)
            {
                return new DeleteCancerTestDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Silme işlemi sırasında bir hata oluştu: {ex.Message}"
                };
            }
        }
    }
}
