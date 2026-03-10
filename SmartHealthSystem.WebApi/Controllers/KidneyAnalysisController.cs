using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.KidneyTestData.Commands.CreateKidneyTestData;
using SmartHealthSystem.Application.Features.KidneyTestData.Commands.DeleteKidneyTestData;
using SmartHealthSystem.Application.Features.KidneyTestData.Commands.UpdateKidneyTestData;
using SmartHealthSystem.Application.Features.KidneyTestData.Queries.GetAllKidneyTestResults;
using SmartHealthSystem.Application.Features.KidneyTestData.Queries.GetKidneyTestResultsByUserId;

namespace SmartHealthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class KidneyAnalysisController : ControllerBase
    {
        private readonly IMediator _mediator;

        public KidneyAnalysisController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Yeni bir böbrek analizi başlatır. Verileri Python AI (8002) servisine gönderir ve sonucu kaydeder.
        /// </summary>
        [HttpPost("create-analysis")]
        public async Task<IActionResult> CreateAnalysis([FromBody] CreateKidneyTestDataCommandRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Geçersiz veri formu." });

            // MediatR sayesinde yazdığımız Kidney Handler otomatik tetiklenir
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }

        // Not: Get, Update ve Delete Query/Command sınıflarını oluşturduğunda 

        // HeartAnalysisController'daki gibi buraya ekleyebilirsin.
        [HttpGet("get-analyses/{patientId}")]
        public async Task<IActionResult> GetPatientAnalyses([FromRoute] string patientId)
        {
            // Query Request nesnesini oluşturup MediatR ile Handler'a gönderiyoruz
            var request = new GetKidneyTestResultsByUserIdQueryRequest { UserId = patientId };
            var response = await _mediator.Send(request);

            // Veri bulunamadıysa 404 dönüyoruz
            if (response == null || !response.Any())
                return NotFound(new { message = "Bu hastaya ait geçmiş böbrek analiz kaydı bulunamadı." });

            return Ok(response);
        }
        [HttpGet("all-technical-data")]
        public async Task<IActionResult> GetAllTechnicalData()
        {
            // GetAllKidneyTestDataQueryRequest üzerinden tüm listeyi istiyoruz
            var response = await _mediator.Send(new GetAllKidneyTestDataQueryRequest());

            if (response == null || !response.Any())
                return Ok(new List<GetAllKidneyTestDataQueryResponse>());

            return Ok(response);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateKidneyTestDataCommandRequest request)
        {
            if (request?.UpdateKidneyTestDataDto == null)
                return BadRequest(new { message = "Güncelleme verisi boş olamaz." });

            // MediatR üzerinden Update Handler tetiklenir
            var response = await _mediator.Send(request);

            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteKidneyTestDataCommandRequest { Id = id });

            // Eğer silme işlemi başarılıysa (200 OK) dön, değilse (404 NotFound) dön.
            if (response.IsSuccess)
            {
                return Ok(response); // Silme başarılıysa artık 404 almazsın
            }

            return NotFound(response);
        }
    }
}
