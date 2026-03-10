using MediatR;
using Microsoft.EntityFrameworkCore;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.Features.CancerTestData.Commands.CreateCancerTestData;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.UpdateCancerTestData
{
    public class UpdateCancerTestDataCommandHandler : IRequestHandler<UpdateCancerTestDataCommandRequest, UpdateCancerTestDataCommandResponse>
    {
        private readonly ICancerTestDataRepository _repository;
        private readonly IHttpClientFactory _httpClientFactory;

        public UpdateCancerTestDataCommandHandler(ICancerTestDataRepository repository, IHttpClientFactory httpClientFactory)
        {
            _repository = repository;
            _httpClientFactory = httpClientFactory;
        }

        public async Task<UpdateCancerTestDataCommandResponse> Handle(UpdateCancerTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. ADIM: Mevcut veriyi getir (DTO'daki Id zaten int olduğu için direkt kullanıyoruz)
            var existingData = await _repository.GetWhere(x => x.Id == request.UpdateCancerTestDataDto.Id)
                                                .Include(x => x.TestResult)
                                                .FirstOrDefaultAsync(cancellationToken);

            if (existingData == null)
                return new UpdateCancerTestDataCommandResponse { IsSuccess = false, Message = "Hata: Güncellenecek analiz bulunamadı." };

            // 2. ADIM: Python AI Servisi İçin Gövdeyi Hazırla
            // C# isimlendirmelerini Python'ın (data.csv) beklediği snake_case formatına çeviriyoruz
            var pythonBody = new
            {
                radius_mean = request.UpdateCancerTestDataDto.RadiusMean,
                texture_mean = request.UpdateCancerTestDataDto.TextureMean,
                perimeter_mean = request.UpdateCancerTestDataDto.PerimeterMean,
                area_mean = request.UpdateCancerTestDataDto.AreaMean,
                smoothness_mean = request.UpdateCancerTestDataDto.SmoothnessMean,
                compactness_mean = request.UpdateCancerTestDataDto.CompactnessMean,
                concavity_mean = request.UpdateCancerTestDataDto.ConcavityMean,
                concave_points_mean = request.UpdateCancerTestDataDto.ConcavePointsMean,
                symmetry_mean = request.UpdateCancerTestDataDto.SymmetryMean,
                fractal_dimension_mean = request.UpdateCancerTestDataDto.FractalDimensionMean
            };

            // 3. ADIM: Python Servisine İstek At (Port 8004)
            var client = _httpClientFactory.CreateClient();
            var response = await client.PostAsJsonAsync("http://127.0.0.1:8004/predict_cancer", pythonBody, cancellationToken);

            if (!response.IsSuccessStatusCode)
                return new UpdateCancerTestDataCommandResponse { IsSuccess = false, Message = "Yapay Zeka servisine ulaşılamadı. Python terminalini kontrol edin." };

            var aiResult = await response.Content.ReadFromJsonAsync<CancerPythonResponse>(cancellationToken);

            // 4. ADIM: Teknik Verileri ve Test Sonucunu Güncelle
            var dto = request.UpdateCancerTestDataDto;

            // Teknik Tablo Güncelleme
            existingData.RadiusMean = dto.RadiusMean;
            existingData.TextureMean = dto.TextureMean;
            existingData.PerimeterMean = dto.PerimeterMean;
            existingData.AreaMean = dto.AreaMean;
            existingData.SmoothnessMean = dto.SmoothnessMean;
            existingData.CompactnessMean = dto.CompactnessMean;
            existingData.ConcavityMean = dto.ConcavityMean;
            existingData.ConcavePointsMean = dto.ConcavePointsMean;
            existingData.SymmetryMean = dto.SymmetryMean;
            existingData.FractalDimensionMean = dto.FractalDimensionMean;
            existingData.UpdatedDate = DateTime.Now;

            // TestResult (İlişkili Tablo) Güncelleme
            if (existingData.TestResult != null && aiResult != null)
            {
                existingData.TestResult.RiskPercentage = aiResult.RiskPercentage;
                existingData.TestResult.HealthScore = (int)aiResult.HealthScore; // Double -> Int cast
                existingData.TestResult.Recommendations = aiResult.Recommendations;
                existingData.TestResult.UpdatedDate = DateTime.Now;
            }

            // 5. ADIM: Değişiklikleri Kaydet
            await _repository.UpdateAsync(existingData);

            return new UpdateCancerTestDataCommandResponse
            {
                IsSuccess = true,
                Message = "Analiz ve yapay zeka sonuçları başarıyla güncellendi.",
                NewRiskPercentage = aiResult?.RiskPercentage ?? 0
            };
        }
    }
}
