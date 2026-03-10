using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.Features.HeartTestData.Commands.CreateHeartTestData;
using SmartHealthSystem.Domain.Entites;
using System;
using System.Net.Http.Json;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.UpdateHeartTestData
{
    public class UpdateHeartTestDataCommandHandler : IRequestHandler<UpdateHeartTestDataCommandRequest, UpdateHeartTestDataCommandResponse>
    {
        private readonly IHeartDiseaseRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateHeartTestDataCommandHandler(IHeartDiseaseRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UpdateHeartTestDataCommandResponse> Handle(UpdateHeartTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. ADIM: Mevcut veriyi ve ilişkili TestResult nesnesini Tracking (Takip) açık şekilde getir.
            // (Repository'deki GetWhere metodundan .AsNoTracking() silindiği için artık EF değişiklikleri algılayacak.)
            var existingData = await _repository.GetWhere(x => x.Id == int.Parse(request.UpdateHeartTestDataDto.Id))
                                                .Include(x => x.TestResult)
                                                .FirstOrDefaultAsync(cancellationToken);

            if (existingData == null)
                return new UpdateHeartTestDataCommandResponse { IsSuccess = false, Message = "Hata: Güncellenecek kayıt bulunamadı." };

            // 2. ADIM: Python AI Servisinden güncel analizi al
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("http://127.0.0.1:8001/predict", request.UpdateHeartTestDataDto, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new UpdateHeartTestDataCommandResponse { IsSuccess = false, Message = "AI Servis hatası: Tahmin alınamadı." };

            var aiResult = await response.Content.ReadFromJsonAsync<PythonPredictionResponse>(cancellationToken);
            if (aiResult == null)
                return new UpdateHeartTestDataCommandResponse { IsSuccess = false, Message = "AI sonuçları boş döndü." };

            // 3. ADIM: HeartTestData (Teknik Tablo) Değerlerini Güncelle
            existingData.Age = request.UpdateHeartTestDataDto.Age;
            existingData.Sex = request.UpdateHeartTestDataDto.Sex;
            existingData.Cp = request.UpdateHeartTestDataDto.Cp;
            existingData.Trestbps = request.UpdateHeartTestDataDto.Trestbps;
            existingData.Chol = request.UpdateHeartTestDataDto.Chol;
            existingData.Fbs = request.UpdateHeartTestDataDto.Fbs;
            existingData.Restecg = request.UpdateHeartTestDataDto.Restecg;
            existingData.Thalach = request.UpdateHeartTestDataDto.Thalach;
            existingData.Exang = request.UpdateHeartTestDataDto.Exang;
            existingData.Oldpeak = (float)request.UpdateHeartTestDataDto.Oldpeak;
            existingData.Slope = request.UpdateHeartTestDataDto.Slope;
            existingData.Ca = request.UpdateHeartTestDataDto.Ca;
            existingData.Thal = request.UpdateHeartTestDataDto.Thal;
            existingData.UpdatedDate = DateTime.Now;

            // 4. ADIM: TestResult (Analiz Tablosu) Değerlerini Güncelle
            // EF Core Include ile bu nesneyi getirdiği için üzerinde yaptığın değişiklik otomatik kaydedilir.
            if (existingData.TestResult != null)
            {
                existingData.TestResult.RiskPercentage = aiResult.RiskPercentage;
                existingData.TestResult.HealthScore = aiResult.HealthScore;
                existingData.TestResult.Recommendations = aiResult.Recommendations;
                existingData.TestResult.UpdatedDate = DateTime.Now;
            }

            // 5. ADIM: Veritabanına Yansıt
            // EF Core Tracking sayesinde hem HeartTestData hem TestResult tablolarına UPDATE sorgusu atılacaktır.
            await _repository.UpdateAsync(existingData);

            return new UpdateHeartTestDataCommandResponse
            {
                IsSuccess = true,
                Message = "Tıbbi veriler ve AI sonuçları başarıyla güncellendi.",
                NewRiskPercentage = aiResult.RiskPercentage
            };
        }
    }
}