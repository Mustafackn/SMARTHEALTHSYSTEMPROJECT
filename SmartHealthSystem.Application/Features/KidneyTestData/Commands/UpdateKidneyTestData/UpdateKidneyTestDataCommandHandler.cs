using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.Features.HeartTestData.Commands.CreateHeartTestData;
using SmartHealthSystem.Application.Features.KidneyTestData.Commands.CreateKidneyTestData;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.UpdateKidneyTestData
{
    public class UpdateKidneyTestDataCommandHandler : IRequestHandler<UpdateKidneyTestDataCommandRequest, UpdateKidneyTestDataCommandResponse>
    {
        private readonly IKidneyDiseaseRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateKidneyTestDataCommandHandler(IKidneyDiseaseRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UpdateKidneyTestDataCommandResponse> Handle(UpdateKidneyTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. ADIM: Mevcut veriyi getir
            var existingData = await _repository.GetWhere(x => x.Id == int.Parse(request.UpdateKidneyTestDataDto.Id))
                                                .Include(x => x.TestResult)
                                                .FirstOrDefaultAsync(cancellationToken);

            if (existingData == null)
                return new UpdateKidneyTestDataCommandResponse { IsSuccess = false, Message = "Hata: Güncellenecek böbrek analizi bulunamadı." };

            // 2. ADIM: Python AI Servisinden güncel analizi al
            var client = _httpClientFactory.CreateClient();

            // Create handler'ındaki gibi 8002 portu ve "/predict" endpoint'ini kullanıyoruz
            // URL'deki "_kidney" kısmını sildik çünkü Python tarafı 404 dönüyor.
            var response = await client.PostAsJsonAsync("http://127.0.0.1:8002/predict", request.UpdateKidneyTestDataDto, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new UpdateKidneyTestDataCommandResponse { IsSuccess = false, Message = "AI Servis hatası: Python servisi yolu bulamadı (404) veya servis kapalı." };

            // Karşılayıcı sınıfı Create'teki gibi KidneyPythonPredictionResponse yaptık
            var aiResult = await response.Content.ReadFromJsonAsync<KidneyPythonPredictionResponse>(cancellationToken);

            if (aiResult == null)
                return new UpdateKidneyTestDataCommandResponse { IsSuccess = false, Message = "AI sonuçları boş döndü." };

            // 3. ADIM: Teknik Tabloyu Güncelle
            existingData.Age = request.UpdateKidneyTestDataDto.Age;
            existingData.Bp = request.UpdateKidneyTestDataDto.Bp;
            existingData.Sg = request.UpdateKidneyTestDataDto.Sg;
            existingData.Al = request.UpdateKidneyTestDataDto.Al;
            existingData.Su = request.UpdateKidneyTestDataDto.Su;
            existingData.Bgr = request.UpdateKidneyTestDataDto.Bgr;
            existingData.Bu = request.UpdateKidneyTestDataDto.Bu;
            existingData.Sc = request.UpdateKidneyTestDataDto.Sc;
            existingData.Sod = request.UpdateKidneyTestDataDto.Sod;
            existingData.Pot = request.UpdateKidneyTestDataDto.Pot;
            existingData.Hemo = request.UpdateKidneyTestDataDto.Hemo;
            existingData.UpdatedDate = DateTime.Now;

            // 4. ADIM: TestResult (Genel Sonuç) Güncelle
            if (existingData.TestResult != null)
            {
                existingData.TestResult.RiskPercentage = aiResult.RiskPercentage;
                existingData.TestResult.HealthScore = aiResult.HealthScore;
                existingData.TestResult.Recommendations = aiResult.Recommendations;
                existingData.TestResult.UpdatedDate = DateTime.Now;
            }

            // 5. ADIM: Kaydet
            await _repository.UpdateAsync(existingData);

            return new UpdateKidneyTestDataCommandResponse
            {
                IsSuccess = true,
                Message = "Böbrek analizi ve yapay zeka sonuçları başarıyla güncellendi.",
                NewRiskPercentage = aiResult.RiskPercentage
            };
        }
    }
}
