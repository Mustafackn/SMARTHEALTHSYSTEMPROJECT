using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.CreateCardiovascularTestData;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.UpdateCardiovascularTestData
{
    public class UpdateCardiovascularTestDataCommandHandler : IRequestHandler<UpdateCardiovascularTestDataCommandRequest, UpdateCardiovascularTestDataCommandResponse>
    {
        private readonly ICardiovascularRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateCardiovascularTestDataCommandHandler(ICardiovascularRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UpdateCardiovascularTestDataCommandResponse> Handle(UpdateCardiovascularTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Mevcut veriyi ve ilişkili TestResult'ı getir
            var existingData = await _repository.GetWhere(x => x.Id == request.UpdateCardioDto.Id)
                                                .Include(x => x.TestResult)
                                                .FirstOrDefaultAsync(cancellationToken);

            if (existingData == null)
                return new UpdateCardiovascularTestDataCommandResponse { IsSuccess = false, Message = "Analiz kaydı bulunamadı." };

            try
            {
                // 2. Python API (8001) Hazırlığı
                var dto = request.UpdateCardioDto;
                var pythonBody = new
                {
                    age = dto.Age,
                    gender = dto.Gender,
                    height = dto.Height,
                    weight = dto.Weight,
                    ap_hi = dto.SystolicBP,
                    ap_lo = dto.DiastolicBP,
                    cholesterol = dto.Cholesterol,
                    gluc = dto.Glucose,
                    smoke = dto.Smoke,
                    alco = dto.Alco,
                    active = dto.Active
                };

                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("http://127.0.0.1:8001/predict-cardio", pythonBody, cancellationToken);

                if (!response.IsSuccessStatusCode)
                    return new UpdateCardiovascularTestDataCommandResponse { IsSuccess = false, Message = "AI Servisi yanıt vermedi." };

                var aiResult = await response.Content.ReadFromJsonAsync<PythonCardioResponse>(cancellationToken);

                // 3. Teknik Verileri Güncelle
                existingData.Age = dto.Age;
                existingData.Height = dto.Height;
                existingData.Weight = dto.Weight;
                existingData.SystolicBP = dto.SystolicBP;
                existingData.DiastolicBP = dto.DiastolicBP;
                existingData.Cholesterol = dto.Cholesterol;
                existingData.Glucose = dto.Glucose;
                existingData.IsSmoking = dto.Smoke == 1;
                existingData.IsAlcoholConsumer = dto.Alco == 1;
                existingData.IsActive = dto.Active == 1;
                existingData.UpdatedDate = DateTime.Now;

                // 4. Test Sonuçlarını Güncelle
                if (existingData.TestResult != null)
                {
                    existingData.TestResult.RiskPercentage = aiResult.RiskPercentage * 100;
                    existingData.TestResult.HealthScore = aiResult.HealthScore;
                    existingData.TestResult.Recommendations = aiResult.Recommendations;
                    existingData.TestResult.UpdatedDate = DateTime.Now;
                }

                await _repository.UpdateAsync(existingData);

                return new UpdateCardiovascularTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = "Analiz başarıyla güncellendi ve risk puanı yeniden hesaplandı.",
                    RiskPercentage = existingData.TestResult?.RiskPercentage ?? 0,
                    HealthScore = (int)(existingData.TestResult?.HealthScore ?? 0),
                    Recommendations = existingData.TestResult?.Recommendations ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                return new UpdateCardiovascularTestDataCommandResponse { IsSuccess = false, Message = $"Hata: {ex.Message}" };
            }
        }
    }
}
