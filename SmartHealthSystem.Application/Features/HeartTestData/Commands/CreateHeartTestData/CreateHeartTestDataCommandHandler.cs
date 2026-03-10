using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Domain.Indetity;
using System;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;

namespace SmartHealthSystem.Application.Features.HeartTestData.Commands.CreateHeartTestData
{
    // Python API'den gelen veriyi karşılayan ara sınıf
    public class PythonPredictionResponse
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

    public class CreateHeartTestDataCommandHandler : IRequestHandler<CreateHeartTestDataCommandRequest, CreateHeartTestDataCommandResponse>
    {
        private readonly IHeartDiseaseRepository _repository;
        private readonly IGenericRepository<TestResult> _testResultRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;

        public CreateHeartTestDataCommandHandler(
            IHeartDiseaseRepository repository,
            IGenericRepository<TestResult> testResultRepository,
            IHttpClientFactory httpClientFactory,
            UserManager<AppUser> userManager)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public async Task<CreateHeartTestDataCommandResponse> Handle(CreateHeartTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Kullanıcı Kontrolü
            var user = await _userManager.FindByIdAsync(request.PatientId);
            if (user == null)
            {
                return new CreateHeartTestDataCommandResponse { IsSuccess = false, Message = "Hata: Hasta bulunamadı." };
            }

            try
            {
                // 2. Python API Hazırlığı ve İstek
                var client = _httpClientFactory.CreateClient();
                var pythonBody = new
                {
                    age = request.Age,
                    sex = request.Sex,
                    cp = request.Cp,
                    trestbps = request.Trestbps,
                    chol = request.Chol,
                    fbs = request.Fbs,
                    restecg = request.Restecg,
                    thalach = request.Thalach,
                    exang = request.Exang,
                    oldpeak = request.Oldpeak,
                    slope = request.Slope,
                    ca = request.Ca,
                    thal = request.Thal
                };

                var response = await client.PostAsJsonAsync("http://127.0.0.1:8001/predict", pythonBody);

                if (!response.IsSuccessStatusCode)
                {
                    return new CreateHeartTestDataCommandResponse { IsSuccess = false, Message = "AI Servisi hata döndürdü." };
                }

                var aiResult = await response.Content.ReadFromJsonAsync<PythonPredictionResponse>();

                if (aiResult == null)
                {
                    return new CreateHeartTestDataCommandResponse { IsSuccess = false, Message = "AI Servisinden veri okunamadı." };
                }

                // 3. Veritabanı Kaydı (TestResult)
                var testResult = new TestResult
                {
                    UserId = Guid.Parse(user.Id.ToString()),
                    RiskPercentage = aiResult.RiskPercentage,
                    HealthScore = aiResult.HealthScore,
                    Recommendations = aiResult.Recommendations,
                    TestTypeId = 1,
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                await _testResultRepository.AddAsync(testResult);

                // 4. Teknik Veri Kaydı (HeartTestData)
                var heartData = new SmartHealthSystem.Domain.Entites.HeartTestData
                {
                    TestResultId = testResult.Id,
                    Age = request.Age,
                    Sex = request.Sex,
                    Cp = request.Cp,
                    Trestbps = request.Trestbps,
                    Chol = request.Chol,
                    Fbs = request.Fbs,
                    Restecg = request.Restecg,
                    Thalach = request.Thalach,
                    Exang = request.Exang,
                    Oldpeak = (float)request.Oldpeak,
                    Slope = request.Slope,
                    Ca = request.Ca,
                    Thal = request.Thal
                };

                await _repository.AddAsync(heartData);

                // 5. SONUÇ: Swagger ve UI'ın beklediği DOLU response paketi
                return new CreateHeartTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = $"{user.FirstName} {user.LastName} için analiz başarıyla tamamlandı.",
                    RiskPercentage = aiResult.RiskPercentage,
                    HealthScore = (int)aiResult.HealthScore,
                    Recommendations = aiResult.Recommendations
                };
            }
            catch (Exception ex)
            {
                return new CreateHeartTestDataCommandResponse { IsSuccess = false, Message = $"Sistem Hatası: {ex.Message}" };
            }
        }
    }
}