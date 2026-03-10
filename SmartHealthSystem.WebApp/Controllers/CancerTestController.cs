using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Contracts;
using SmartHealthSystem.Application.DTOs.CancerTestData;
using SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers;
using SmartHealthSystem.Application.Features.CancerTestData.Commands.CreateCancerTestData;
using SmartHealthSystem.Application.Features.CancerTestData.Commands.UpdateCancerTestData;

namespace SmartHealthSystem.WebApp.Controllers
{
    public class CancerTestController : Controller
    {
        private readonly IMediator _mediator;
        // 1. Repository tanımını ekliyoruz
        private readonly ITestResultRepository _testResultRepository;

        // 2. Constructor içinde her ikisini de sisteme dahil ediyoruz
        public CancerTestController(IMediator mediator, ITestResultRepository testResultRepository)
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

        [HttpPost]
        public async Task<IActionResult> CreateAnalysis([FromBody] CreateCancerTestDataCommandRequest request)
        {
            if (request == null) return BadRequest(new { message = "Veriler boş." });
            var result = await _mediator.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(new { message = result.Message });
        }

        [HttpGet]
        public async Task<IActionResult> Update(int testResultId)
        {
            // 3. Repository üzerinden teknik verileri çekiyoruz
            var data = await _testResultRepository.GetCancerDataByTestResultIdAsync(testResultId);

            if (data == null) return NotFound("Analiz verisi bulunamadı.");

            // 4. Veritabanı nesnesini DTO'ya mapliyoruz (Tüm alanları ekledim)
            var model = new UpdateCancerTestDataDto
            {
                Id = data.Id,
                RadiusMean = data.RadiusMean,
                TextureMean = data.TextureMean,
                PerimeterMean = data.PerimeterMean,
                AreaMean = data.AreaMean,
                SmoothnessMean = data.SmoothnessMean,
                CompactnessMean = data.CompactnessMean,
                ConcavityMean = data.ConcavityMean,
                ConcavePointsMean = data.ConcavePointsMean,
                SymmetryMean = data.SymmetryMean,
                FractalDimensionMean = data.FractalDimensionMean
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromBody] UpdateCancerTestDataCommandRequest request)
        {
            // Senin yazdığın Python AI destekli Update Handler tetiklenir
            var result = await _mediator.Send(request);
            return result.IsSuccess ? Ok(result) : BadRequest(new { message = result.Message });
        }

    }
}
