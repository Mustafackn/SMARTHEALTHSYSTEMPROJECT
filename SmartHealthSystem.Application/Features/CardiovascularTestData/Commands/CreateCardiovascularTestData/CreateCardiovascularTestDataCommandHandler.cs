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

namespace SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.CreateCardiovascularTestData
{
    public class PythonCardioResponse
    {
        [JsonPropertyName("cardio_probability")]
        public double RiskPercentage { get; set; }

        [JsonPropertyName("general_health_score")]
        public int HealthScore { get; set; }

        [JsonPropertyName("personalized_recommendations")]
        public List<string> RecommendationsList { get; set; } = new();

        public string Recommendations => string.Join(" | ", RecommendationsList);
    }

    public class CreateCardiovascularTestDataCommandHandler : IRequestHandler<CreateCardiovascularTestDataCommandRequest, CreateCardiovascularTestDataCommandResponse>
    {
        private readonly ICardiovascularRepository _repository;
        private readonly IGenericRepository<TestResult> _testResultRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;

        public CreateCardiovascularTestDataCommandHandler(
            ICardiovascularRepository repository,
            IGenericRepository<TestResult> testResultRepository,
            IHttpClientFactory httpClientFactory,
            UserManager<AppUser> userManager)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public async Task<CreateCardiovascularTestDataCommandResponse> Handle(CreateCardiovascularTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByIdAsync(request.UserId.ToString());
            if (user == null) return new CreateCardiovascularTestDataCommandResponse { IsSuccess = false, Message = "Kullanıcı bulunamadı." };

            try
            {
                // 1. Python API (8001 Portu) Hazırlığı
                var client = _httpClientFactory.CreateClient();
                var pythonBody = new
                {
                    age = request.Age,
                    gender = request.Gender,
                    height = request.Height,
                    weight = request.Weight,
                    ap_hi = request.SystolicBP,
                    ap_lo = request.DiastolicBP,
                    cholesterol = request.Cholesterol,
                    gluc = request.Glucose,
                    smoke = request.Smoke,
                    alco = request.Alco,
                    active = request.Active
                };

                var response = await client.PostAsJsonAsync("http://127.0.0.1:8001/predict-cardio", pythonBody);
                if (!response.IsSuccessStatusCode)
                    return new CreateCardiovascularTestDataCommandResponse { IsSuccess = false, Message = "AI Servisi hata döndürdü." };

                var aiResult = await response.Content.ReadFromJsonAsync<PythonCardioResponse>();
                if (aiResult == null) return new CreateCardiovascularTestDataCommandResponse { IsSuccess = false, Message = "Veri alınamadı." };

                // 2. Merkezi TestResult Kaydı
                var testResult = new TestResult
                {
                    UserId = Guid.Parse(user.Id.ToString()),
                    RiskPercentage = aiResult.RiskPercentage,
                    HealthScore = aiResult.HealthScore,
                    Recommendations = aiResult.Recommendations,
                    TestTypeId = 5, // Veritabanına yeni eklediğimiz ID
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };
                await _testResultRepository.AddAsync(testResult);

                // 3. Teknik Verilerin Kaydı
                var cardioData = new Domain.Entites.CardiovascularTestData
                {
                    TestResultId = testResult.Id,
                    Age = request.Age,
                    Gender = request.Gender,
                    Height = request.Height,
                    Weight = request.Weight,
                    SystolicBP = request.SystolicBP,
                    DiastolicBP = request.DiastolicBP,
                    Cholesterol = request.Cholesterol,
                    Glucose = request.Glucose,
                    IsSmoking = request.Smoke == 1,
                    IsAlcoholConsumer = request.Alco == 1,
                    IsActive = request.Active == 1
                };
                await _repository.AddAsync(cardioData);

                return new CreateCardiovascularTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = "Kardiyovasküler analiz tamamlandı.",
                    RiskPercentage = testResult.RiskPercentage,
                    HealthScore = (int)testResult.HealthScore,
                    Recommendations = testResult.Recommendations
                };
            }
            catch (Exception ex)
            {
                return new CreateCardiovascularTestDataCommandResponse { IsSuccess = false, Message = ex.Message };
            }
        }
    }
}
