using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.DTOs.HeratTestData;
using SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers;
using SmartHealthSystem.Application.Features.HeartTestData.Commands.CreateHeartTestData;
using SmartHealthSystem.Application.Features.HeartTestData.Commands.UpdateHeartTestData;
using SmartHealthSystem.Application.Features.HeartTestData.Queries.GetAllHeartTestResults;
using SmartHealthSystem.Application.Features.HeartTestData.Queries.GetHeartTestResultsByUserId;
using SmartHealthSystem.WebApp.Models;
using System.Net.Http.Json;

namespace SmartHealthSystem.WebApp.Controllers
{
    public class HearthTestController : Controller
    {
        private readonly IMediator _mediator;
        private readonly ITestResultRepository _testResultRepository; // Repository eklendi

        public HearthTestController(IMediator mediator, ITestResultRepository testResultRepository)
        {
            _mediator = mediator;
            _testResultRepository = testResultRepository;
        }

        // --- MEVCUT METOTLARIN (Index, Create vb.) DEVAMI ---
        public async Task<IActionResult> Index()
        {
            var query = new GetUsersQueryRequest();
            var patients = await _mediator.Send(query);
            return View(patients);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAnalysis([FromBody] CreateHeartTestDataCommandRequest request)
        {
            if (request == null) return BadRequest(new { message = "Geçersiz veri." });
            var result = await _mediator.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(new { message = result.Message });
        }

        // --- GÜNCELLEME (UPDATE) METOTLARI ---

        [HttpGet]
        public async Task<IActionResult> Update(int testResultId)
        {
            // Repository üzerinden Kalp teknik verilerini çekiyoruz
            var data = await _testResultRepository.GetHeartDataByTestResultIdAsync(testResultId);

            if (data == null) return NotFound("Kalp analiz verisi bulunamadı.");

            // View'a gönderilecek DTO eşlemesi 
            var model = new UpdateHeartTestDataDto
            {
                Id = data.Id.ToString(),
                Age = data.Age,
                Sex = data.Sex,
                Cp = data.Cp,
                Trestbps = data.Trestbps,
                Chol = data.Chol,
                Fbs = data.Fbs,
                Restecg = data.Restecg,
                Thalach = data.Thalach,
                Exang = data.Exang,
                Oldpeak = data.Oldpeak,
                Slope = data.Slope,
                Ca = data.Ca,
                Thal = data.Thal
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateHeartTestDataCommandRequest request)
        {
            if (request == null || request.UpdateHeartTestDataDto == null)
                return BadRequest(new { message = "İstek verisi boş (null) geldi." });

            var result = await _mediator.Send(request);

            if (result.IsSuccess)
                return Ok(result);

            return BadRequest(new { message = result.Message });
        }

        // Diğer listeleme metotların...
        [HttpGet]
        public IActionResult ListResult() => View();
    }
}
