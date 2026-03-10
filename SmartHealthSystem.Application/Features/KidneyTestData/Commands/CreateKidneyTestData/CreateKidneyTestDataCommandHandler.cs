using MediatR;
using Microsoft.AspNetCore.Identity;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Domain.Entites;
using SmartHealthSystem.Domain.Indetity;
using System.Net.Http.Json;
using System.Text.Json.Serialization;

namespace SmartHealthSystem.Application.Features.KidneyTestData.Commands.CreateKidneyTestData
{
    // Python'dan gelen veriyi eksiksiz karşılamak için sınıf
    public class KidneyPythonPredictionResponse
    {
        [JsonPropertyName("is_at_risk")]
        public int IsAtRisk { get; set; }

        [JsonPropertyName("risk_percentage")]
        public double RiskPercentage { get; set; } // Hassas yüzdelik değer

        [JsonPropertyName("health_score")]
        public double HealthScore { get; set; }

        [JsonPropertyName("recommendations")]
        public string Recommendations { get; set; } = string.Empty;
    }

    public class CreateKidneyTestDataCommandHandler : IRequestHandler<CreateKidneyTestDataCommandRequest, CreateKidneyTestDataCommandResponse>
    {
        private readonly IKidneyDiseaseRepository _repository;
        private readonly IGenericRepository<TestResult> _testResultRepository;
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly UserManager<AppUser> _userManager;

        public CreateKidneyTestDataCommandHandler(
            IKidneyDiseaseRepository repository,
            IGenericRepository<TestResult> testResultRepository,
            IHttpClientFactory httpClientFactory,
            UserManager<AppUser> userManager)
        {
            _repository = repository;
            _testResultRepository = testResultRepository;
            _httpClientFactory = httpClientFactory;
            _userManager = userManager;
        }

        public async Task<CreateKidneyTestDataCommandResponse> Handle(CreateKidneyTestDataCommandRequest request, CancellationToken cancellationToken)
        {
            // 1. Kullanıcı Kontrolü ve GUID Validasyonu
            var user = await _userManager.FindByIdAsync(request.PatientId);
            if (user == null)
            {
                return new CreateKidneyTestDataCommandResponse { IsSuccess = false, Message = "Hata: Hasta bulunamadı." };
            }

            try
            {
                var client = _httpClientFactory.CreateClient();

                // Python servisine gidecek body (Hassas double değerler ile)
                var pythonBody = new
                {
                    age = request.Age,
                    bp = request.Bp,
                    sg = request.Sg,
                    al = request.Al,
                    su = request.Su,
                    rbc = request.Rbc,
                    pc = request.Pc,
                    pcc = request.Pcc,
                    ba = request.Ba,
                    bgr = request.Bgr,
                    bu = request.Bu,
                    sc = request.Sc,
                    sod = request.Sod,
                    pot = request.Pot,
                    hemo = request.Hemo
                };

                // Python 8002 portuna istek atıyoruz
                var response = await client.PostAsJsonAsync("http://127.0.0.1:8002/predict", pythonBody);

                if (!response.IsSuccessStatusCode)
                {
                    return new CreateKidneyTestDataCommandResponse { IsSuccess = false, Message = "Yapay Zeka servisine ulaşılamıyor." };
                }

                var aiResult = await response.Content.ReadFromJsonAsync<KidneyPythonPredictionResponse>();

                if (aiResult == null)
                {
                    return new CreateKidneyTestDataCommandResponse { IsSuccess = false, Message = "AI verisi okunamadı." };
                }

                // 2. Veritabanı Kaydı (TestResults Tablosu)
                var testResult = new TestResult
                {
                    UserId = Guid.Parse(user.Id.ToString()), // Identity'den gelen GUID stringi parse ediyoruz
                    RiskPercentage = aiResult.RiskPercentage, // Veritabanına %12.5 gibi kaydedilir
                    HealthScore = aiResult.HealthScore,
                    Recommendations = aiResult.Recommendations,
                    TestTypeId = 2, // Böbrek Analizi
                    CreatedDate = DateTime.Now,
                    IsActive = true
                };

                await _testResultRepository.AddAsync(testResult);

                // 3. KidneyTestData Tablosuna Detay Kaydı
                var kidneyData = new SmartHealthSystem.Domain.Entites.KidneyTestData
                {
                    TestResultId = testResult.Id,
                    Age = request.Age,
                    Bp = request.Bp,
                    Sg = request.Sg,
                    Al = request.Al,
                    Su = request.Su,
                    Rbc = request.Rbc,
                    Pc = request.Pc,
                    Pcc = request.Pcc,
                    Ba = request.Ba,
                    Bgr = request.Bgr,
                    Bu = request.Bu,
                    Sc = request.Sc,
                    Sod = request.Sod,
                    Pot = request.Pot,
                    Hemo = request.Hemo
                };

                await _repository.AddAsync(kidneyData);

                // Başarılı Sonuç Dönüyoruz
                return new CreateKidneyTestDataCommandResponse
                {
                    IsSuccess = true,
                    Message = $"{user.FirstName} {user.LastName} için analiz başarıyla tamamlandı.",
                    RiskPercentage = aiResult.RiskPercentage,
                    HealthScore = aiResult.HealthScore,
                    Recommendations = aiResult.Recommendations,
              
                };
            }
            catch (Exception ex)
            {
                return new CreateKidneyTestDataCommandResponse { IsSuccess = false, Message = $"Sistem Hatası: {ex.Message}" };
            }
        }
    }
}