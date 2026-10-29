using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using System;
using System.Collections.Generic;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.DeleteDiabetesTestData
{
    public class DeleteDiabetesTestDataCommandHandler : IRequestHandler<DeleteDiabetesTestDataCommandRequest, DeleteDiabetesTestDataCommandResponse>
    {
        private readonly IDiabetesRepository _repository;
        private readonly IGenericRepository<TestResult> _testResultRepository;

        public DeleteDiabetesTestDataCommandHandler(IDiabetesRepository repository, IGenericRepository<TestResult> testResultRepository)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
        }

        public async Task<DeleteDiabetesTestDataCommandResponse> Handle(DeleteDiabetesTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. ADIM: İlişkili TestResult ile birlikte kaydı getir
            var dataToDelete = await _repository.GetWhere(x => x.Id == request.Id)
                                                .Include(x => x.TestResult)
                                                .FirstOrDefaultAsync(cancellationToken);

            // 2. ADIM: Kayıt kontrolü
            if (dataToDelete == null)
            {
                return new DeleteDiabetesTestDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Hata: {request.Id} numaralı diyabet analizi sistemde bulunamadı."
                };
            }

            try
            {
                // 3. ADIM: Önce bağımlı AI sonucunu (TestResult) sil
                if (dataToDelete.TestResult != null)
                {
                    await _testResultRepository.DeleteAsync(dataToDelete.TestResult);
                }

                // 4. ADIM: Ana teknik veriyi (DiabetesTestData) sil
                await _repository.DeleteAsync(dataToDelete);

                return new DeleteDiabetesTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = "Diyabet analizi ve ilişkili tüm sonuçlar başarıyla temizlendi."
                };
            }
            catch (Exception ex)
            {
                return new DeleteDiabetesTestDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Silme işlemi başarısız oldu: {ex.Message}"
                };
            }
        }
    }
}
