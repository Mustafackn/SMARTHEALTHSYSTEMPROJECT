using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers;
using SmartHealthSystem.Application.Features.HeartTestData.Queries.GetHeartTestResultsByUserId;
using SmartHealthSystem.Application.Features.TestResultsData;
using SmartHealthSystem.Application.Features.TestResultsData.Query;

namespace SmartHealthSystem.WebApp.Controllers
{
    public class PatientController : Controller
    {
        private readonly IMediator _mediator;
        // 1. Repository'yi buraya ekliyoruz
        private readonly ITestResultRepository _testResultRepository;

        // 2. Constructor (Yapıcı Metot) içinde her ikisini de istiyoruz
        public PatientController(IMediator mediator, ITestResultRepository testResultRepository)
        {
            _mediator = mediator;
            _testResultRepository = testResultRepository;
        }

        public async Task<IActionResult> Index()
        {
            var query = new GetUsersQueryRequest();
            var patients = await _mediator.Send(query);
            return View(patients);
        }

        [HttpGet]
        public async Task<IActionResult> PatientHistory(string id)
        {
            if (string.IsNullOrEmpty(id)) return RedirectToAction("Index");

            var query = new GetPatientTestResultsQueryRequest { UserId = Guid.Parse(id) };
            var response = await _mediator.Send(query);

            return View(response.Results ?? new List<SmartHealthSystem.Application.DTOs.TestResultDto.TestResultListDto>());
        }

        [HttpGet]
        public async Task<IActionResult> RedirectToUpdate(int id)
        {
            // 3. Artık _testResultRepository burada hata vermeyecek!
            var testResult = await _testResultRepository.GetTestResultWithDetailsByIdAsync(id);

            if (testResult == null) return NotFound();

            // Veritabanındaki TestName değerlerinin şunlarla tam eşleştiğinden emin ol:
            return testResult.TestType.TestName switch
            {
                // Ekran görüntüsündeki "Analiz Türü" sütunundaki isimlerle birebir eşitledim:
                "Meme Kanseri Analizi" => RedirectToAction("Update", "CancerTest", new { testResultId = id }),

                // "Sağlığı" kelimesini sildim, çünkü tabloda sadece "Kalp Analizi" yazıyor
                "Kalp Analizi" => RedirectToAction("Update", "HearthTest", new { testResultId = id }),

                // Hem ismi düzelttim hem de Controller adını senin dosyan olan "KindTest" yaptım
                "Böbrek Analizi" => RedirectToAction("Update", "KindTest", new { testResultId = id }),

                _ => RedirectToAction("PatientHistory", new { id = testResult.UserId.ToString() })
            };
        }
    }
}
