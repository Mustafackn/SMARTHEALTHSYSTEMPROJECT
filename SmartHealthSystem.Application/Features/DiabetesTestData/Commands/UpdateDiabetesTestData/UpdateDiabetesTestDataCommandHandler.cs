using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.Features.DiabetesTestData.Commands.CreateDiabetesTestData;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.UpdateDiabetesTestData
{
    public class UpdateDiabetesTestDataCommandHandler : IRequestHandler<UpdateDiabetesTestDataCommandRequest, UpdateDiabetesTestDataCommandResponse>
    {
        private readonly IDiabetesRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateDiabetesTestDataCommandHandler(IDiabetesRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UpdateDiabetesTestDataCommandResponse> Handle(UpdateDiabetesTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. ADIM: Mevcut veriyi ve ilişkili TestResult tablosunu getir
            var existingData = await _repository.GetWhere(x => x.Id == request.UpdateDiabetesTestDataDto.Id)
                                                .Include(x => x.TestResult)
                                                .FirstOrDefaultAsync(cancellationToken);

            if (existingData == null)
            {
                return new UpdateDiabetesTestDataCommandResponse
                {
                    IsSuccess = false,
                    Message = "Hata: Güncellenecek analiz kaydı bulunamadı."
                };
            }

            try
            {
                // 2. ADIM: Python API Hazırlığı (Snake Case Uyumu)
                var dto = request.UpdateDiabetesTestDataDto;
                var pythonBody = new
                {
                    gender = dto.Gender,
                    age = dto.Age,
                    hypertension = dto.Hypertension,
                    heart_disease = dto.HeartDisease,
                    smoking_history = dto.SmokingHistory,
                    bmi = dto.BMI,
                    hbA1c_level = dto.HbA1cLevel,
                    blood_glucose_level = dto.BloodGlucoseLevel
                };

                // Python servisine istek at (8000 portu - analyze-health endpointi)
                var client = _httpClientFactory.CreateClient();
                var response = await client.PostAsJsonAsync("http://127.0.0.1:8000/analyze-health", pythonBody, cancellationToken);

                if (!response.IsSuccessStatusCode)
                {
                    return new UpdateDiabetesTestDataCommandResponse { IsSuccess = false, Message = "Yapay Zeka servisi yanıt vermedi. Lütfen Python terminalini kontrol edin." };
                }

                // Python'dan gelen sonucu oku
                var aiResult = await response.Content.ReadFromJsonAsync<PythonPredictionResponse>(cancellationToken);

                if (aiResult == null)
                {
                    return new UpdateDiabetesTestDataCommandResponse { IsSuccess = false, Message = "Analiz verisi AI tarafından işlenemedi." };
                }

                // 3. ADIM: Teknik Verileri Güncelle (DiabetesTestData Tablosu)
                existingData.Gender = dto.Gender;
                existingData.Age = dto.Age;
                existingData.Hypertension = dto.Hypertension;
                existingData.HeartDisease = dto.HeartDisease;
                existingData.SmokingHistory = dto.SmokingHistory;
                existingData.BMI = dto.BMI;
                existingData.HbA1cLevel = dto.HbA1cLevel;
                existingData.BloodGlucoseLevel = dto.BloodGlucoseLevel;
                existingData.UpdatedDate = DateTime.Now;

                // 4. ADIM: Test Sonuçlarını Güncelle (TestResults Tablosu)
                if (existingData.TestResult != null)
                {
                    // AI'dan gelen 0-1 arası değeri yüzdeye çeviriyoruz
                    existingData.TestResult.RiskPercentage = aiResult.RiskPercentage * 100;

                    // KRİTİK DÜZELTME: Double'dan Int'e açık dönüşüm (Cast) yapıldı
                    existingData.TestResult.HealthScore = (int)aiResult.HealthScore;

                    // Önerileri listeye çevirip kaydediyoruz (Eğer DB'de string ise string.Join kullanabilirsin)
                    existingData.TestResult.Recommendations = string.Join(" | ", aiResult.Recommendations);
                    existingData.TestResult.UpdatedDate = DateTime.Now;
                }

                // 5. ADIM: Veritabanına Değişiklikleri Kaydet
                await _repository.UpdateAsync(existingData);

                // 6. ADIM: Başarılı Response Dön (CreateResponse ile aynı yapıda)
                return new UpdateDiabetesTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = "Diyabet analizi başarıyla güncellendi ve risk puanı yeniden hesaplandı.",
                    RiskPercentage = existingData.TestResult?.RiskPercentage ?? 0,
                    // (int) ekleyerek double değeri int'e zorla dönüştürüyoruz
                    HealthScore = (int)(existingData.TestResult?.HealthScore ?? 0),
                    Recommendations = existingData.TestResult?.Recommendations ?? string.Empty
                };
            }
            catch (Exception ex)
            {
                return new UpdateDiabetesTestDataCommandResponse
                {
                    IsSuccess = false,
                    Message = $"Güncelleme sırasında bir sistem hatası oluştu: {ex.Message}"
                };
            }
        }
    }
}
