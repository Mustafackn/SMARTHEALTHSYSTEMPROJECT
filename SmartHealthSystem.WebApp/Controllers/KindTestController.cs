using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.DTOs.KidneyTestData;
using SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers;
using SmartHealthSystem.Application.Features.KidneyTestData.Commands.CreateKidneyTestData;
using SmartHealthSystem.Application.Features.KidneyTestData.Commands.UpdateKidneyTestData;

namespace SmartHealthSystem.WebApp.Controllers
{
    public class KindTestController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ITestResultRepository _testResultRepository; // Veri çekmek için eklendi

        public KindTestController(IMediator mediator, ITestResultRepository testResultRepository)
        {
            _mediator = mediator;
            _testResultRepository = testResultRepository;
        }

        // GET: KindTest/Index
        public async Task<IActionResult> Index()
        {
            var query = new GetUsersQueryRequest();
            var patients = await _mediator.Send(query);
            return View(patients);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnalysis([FromBody] CreateKidneyTestDataCommandRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Geçersiz veri formatı." });

            if (string.IsNullOrEmpty(request.PatientId) || !Guid.TryParse(request.PatientId, out _))
                return BadRequest(new { message = "Geçersiz Hasta seçimi." });

            var result = await _mediator.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(new { message = result.Message });
        }

        // --- UPDATE (GÜNCELLEME) AKSİYONLARI ---

        // 1. Sayfayı açan metot (Verileri yükler)
        [HttpGet]
        public async Task<IActionResult> Update(int testResultId)
        {
            // Repository üzerinden veriyi çekiyoruz
            var data = await _testResultRepository.GetKidneyDataByTestResultIdAsync(testResultId);

            if (data == null)
                return NotFound("Böbrek analiz verisi bulunamadı.");

            // Sınıfındaki (DTO) isimlerin Bp, Sg, Al, Su vb. olduğunu varsayarak eşleştiriyoruz:
            var model = new UpdateKidneyTestDataDto
            {
                Id = data.Id.ToString(), // DTO'da string ise .ToString() ekle
                Age = data.Age,
                Bp = data.Bp,         // BloodPressure yerine Bp
                Sg = data.Sg,         // SpecificGravity yerine Sg
                Al = data.Al,         // Albumin yerine Al
                Su = data.Su,         // Sugar yerine Su
                Rbc = data.Rbc,       // RedBloodCells yerine Rbc
                Pc = data.Pc,         // PusCell yerine Pc
                Pcc = data.Pcc,       // PusCellClumps yerine Pcc
                Ba = data.Ba,         // Bacteria yerine Ba
                Bgr = data.Bgr,       // BloodGlucoseRandom yerine Bgr
                Bu = data.Bu,         // BloodUrea yerine Bu
                Sc = data.Sc,         // SerumCreatinine yerine Sc
                Sod = data.Sod,       // Sodium yerine Sod
                Pot = data.Pot,       // Potassium yerine Pot
                Hemo = data.Hemo      // Hemoglobin yerine Hemo
            };

            return View(model);
        }

        // 2. JavaScript'ten gelen güncellemeyi işleyen metot
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateKidneyTestDataCommandRequest request)
        {
            if (request == null || request.UpdateKidneyTestDataDto == null)
                return BadRequest(new { message = "Güncelleme verisi boş geldi." });

            // MediatR üzerinden Handler'ı tetikliyoruz (Python servisine gider ve DB'yi günceller)
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(new { message = result.Message });
        }
    }
}
