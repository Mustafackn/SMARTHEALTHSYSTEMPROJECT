using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartHealthSystem.Application.Features.CancerTestData.Commands.CreateCancerTestData
{
    public class CancerPythonResponse
    {
        [JsonPropertyName("is_at_risk")]
        public int IsAtRisk { get; set; }

        [JsonPropertyName("risk_percentage")]
        public double RiskPercentage { get; set; }

        [JsonPropertyName("health_score")]
        public double HealthScore { get; set; }

        [JsonPropertyName("recommendations")]
        public string Recommendations { get; set; } = string.Empty;
    }

    public class CreateCancerTestDataCommandHandler : IRequestHandler<CreateCancerTestDataCommandRequest, CreateCancerTestDataCommandResponse>
    {
        private readonly ICancerTestDataRepository _repository;
        private readonly IGenericRepository<TestResult> _testResultRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;

        public CreateCancerTestDataCommandHandler(
            ICancerTestDataRepository repository,
            IGenericRepository<TestResult> testResultRepository,
            IHttpClientFactory httpClientFactory,
            UserManager<AppUser> userManager)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public async Task<CreateCancerTestDataCommandResponse> Handle(CreateCancerTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.PatientId);
            if (user == null) return new CreateCancerTestDataCommandResponse { IsSuccess = false, Message = "Hata: Hasta bulunamadı." };

            try
            {
                var client = _httpClientFactory.CreateClient();

                // Python Port 8004 olarak ayarlandı
                var pythonBody = new
                {
                    radius_mean = request.RadiusMean,
                    texture_mean = request.TextureMean,
                    perimeter_mean = request.PerimeterMean,
                    area_mean = request.AreaMean,
                    smoothness_mean = request.SmoothnessMean,
                    compactness_mean = request.CompactnessMean,
                    concavity_mean = request.ConcavityMean,
                    concave_points_mean = request.ConcavePointsMean,
                    symmetry_mean = request.SymmetryMean,
                    fractal_dimension_mean = request.FractalDimensionMean
                };

                var response = await client.PostAsJsonAsync("http://127.0.0.1:8004/predict_cancer", pythonBody);
                if (!response.IsSuccessStatusCode) return new CreateCancerTestDataCommandResponse { IsSuccess = false, Message = "AI servisine ulaşılamıyor." };

                var aiResult = await response.Content.ReadFromJsonAsync<CancerPythonResponse>();

                // 2. TestResults Kaydı (TestTypeId: 3 - SQL ile güncellediğimiz ID)
                var testResult = new TestResult
                {
                    UserId = Guid.Parse(user.Id.ToString()),
                    RiskPercentage = aiResult.RiskPercentage,
                    HealthScore = aiResult.HealthScore,
                    Recommendations = aiResult.Recommendations,
                    TestTypeId = 3,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                await _testResultRepository.AddAsync(testResult);

                // 3. CancerTestData (Teknik Detay) Kaydı
                var cancerData = new SmartHealthSystem.Domain.Entites.CancerTestData
                {
                    TestResultId = testResult.Id,
                    RadiusMean = request.RadiusMean,
                    TextureMean = request.TextureMean,
                    PerimeterMean = request.PerimeterMean,
                    AreaMean = request.AreaMean,
                    SmoothnessMean = request.SmoothnessMean,
                    CompactnessMean = request.CompactnessMean,
                    ConcavityMean = request.ConcavityMean,
                    ConcavePointsMean = request.ConcavePointsMean,
                    SymmetryMean = request.SymmetryMean,
                    FractalDimensionMean = request.FractalDimensionMean
                };

                await _repository.AddAsync(cancerData);

                return new CreateCancerTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = $"{user.FirstName} {user.LastName} için meme kanseri analizi %95.61 doğrulukla tamamlandı.",
                    RiskPercentage = aiResult.RiskPercentage,
                    HealthScore = aiResult.HealthScore,
                    Recommendations = aiResult.Recommendations
                };
            }
            catch (Exception ex)
            {
                return new CreateCancerTestDataCommandResponse { IsSuccess = false, Message = $"Sistem Hatası: {ex.Message}" };
            }
        }
    }
}
