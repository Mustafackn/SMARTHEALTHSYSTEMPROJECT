using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.Features.HeartTestData.Commands.CreateHeartTestData;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Serialization;

namespace SmartHealthSystem.Application.Features.DiabetesTestData.Commands.CreateDiabetesTestData
{
    // Python API'den gelen veriyi karşılayan ara sınıf (JSON isimlendirmeleri Python ile eşleşmeli)
    public class PythonPredictionResponse
    {
        [JsonPropertyName("diabetes_probability")]
        public double RiskPercentage { get; set; }

        [JsonPropertyName("general_health_score")]
        public double HealthScore { get; set; }

        [JsonPropertyName("personalized_recommendations")]
        public List<string> RecommendationsList { get; set; } = new();

        // Veritabanına kaydederken listeyi string'e çevirmek için yardımcı özellik
        public string Recommendations => string.Join(" | ", RecommendationsList);
    }

    public class CreateDiabetesTestDataCommandHandler : IRequestHandler<CreateDiabetesTestDataCommandRequest, CreateDiabetesTestDataCommandResponse>
    {
        private readonly IDiabetesRepository _repository;
        private readonly IGenericRepository<TestResult> _testResultRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;

        public CreateDiabetesTestDataCommandHandler(
            IDiabetesRepository repository,
            IGenericRepository<TestResult> testResultRepository,
            IHttpClientFactory httpClientFactory,
            UserManager<AppUser> userManager)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public async Task<CreateDiabetesTestDataCommandResponse> Handle(CreateDiabetesTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Kullanıcı Kontrolü
            var user = await _userManager.FindByIdAsync(request.PatientId);
            if (user == null)
            {
                return new CreateDiabetesTestDataCommandResponse { IsSuccess = false, Message = "Hata: Hasta bulunamadı." };
            }

            try
            {
                // 2. Python API Hazırlığı (Python'daki PatientInput sınıfı ile birebir aynı isimler)
                var client = _httpClientFactory.CreateClient();
                var pythonBody = new
                {
                    gender = request.Gender,
                    age = request.Age,
                    hypertension = request.Hypertension,
                    heart_disease = request.HeartDisease, // Python 'heart_disease' bekliyor
                    smoking_history = request.SmokingHistory,
                    bmi = request.BMI,
                    hbA1c_level = request.HbA1cLevel, // Python 'hbA1c_level' bekliyor
                    blood_glucose_level = request.BloodGlucoseLevel // Python 'blood_glucose_level' bekliyor
                };

                // Python kodun 8000 portunda ve "/analyze-health" endpoint'inde çalışıyor
                var response = await client.PostAsJsonAsync("http://127.0.0.1:8000/analyze-health", pythonBody);

                if (!response.IsSuccessStatusCode)
                {
                    return new CreateDiabetesTestDataCommandResponse { IsSuccess = false, Message = "AI Servisi (Python) hata döndürdü." };
                }

                var aiResult = await response.Content.ReadFromJsonAsync<PythonPredictionResponse>();

                if (aiResult == null)
                {
                    return new CreateDiabetesTestDataCommandResponse { IsSuccess = false, Message = "AI Servisinden analiz verisi alınamadı." };
                }

                // 3. Veritabanı Kaydı (Merkezi TestResult tablosu)
                var testResult = new TestResult
                {
                    UserId = Guid.Parse(user.Id.ToString()),
                    RiskPercentage = aiResult.RiskPercentage * 100, // Olasılığı yüzdeye çeviriyoruz (Örn: 0.85 -> 85.0)
                    HealthScore = aiResult.HealthScore,
                    Recommendations = aiResult.Recommendations,
                    TestTypeId = 4, // Veritabanındaki Diabetes TestType ID'si
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                await _testResultRepository.AddAsync(testResult);

                // 4. Diyabet Teknik Verilerinin Kaydı (DiabetesTestData)
                var diabetesData = new SmartHealthSystem.Domain.Entites.DiabetesTestData
                {
                    TestResultId = testResult.Id,
                    Gender = request.Gender,
                    Age = request.Age,
                    Hypertension = request.Hypertension,
                    HeartDisease = request.HeartDisease,
                    SmokingHistory = request.SmokingHistory,
                    BMI = request.BMI,
                    HbA1cLevel = request.HbA1cLevel,
                    BloodGlucoseLevel = request.BloodGlucoseLevel
                };

                await _repository.AddAsync(diabetesData);

                // 5. Başarılı Yanıt Dönüşü
                return new CreateDiabetesTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = $"{user.FirstName} {user.LastName} için diyabet analizi başarıyla tamamlandı.",
                    RiskPercentage = testResult.RiskPercentage,
                    HealthScore = (int)testResult.HealthScore,
                    Recommendations = testResult.Recommendations
                };
            }
            catch (Exception ex)
            {
                return new CreateDiabetesTestDataCommandResponse { IsSuccess = false, Message = $"Sistem Hatası: {ex.Message}" };
            }
        }
    }
}
