using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.DTOs.HeratTestData;
using SmartHealthSystem.Application.Features.HeartTestData.Commands.CreateHeartTestData;
using SmartHealthSystem.Application.Features.HeartTestData.Commands.DeleteHeartTestData;
using SmartHealthSystem.Application.Features.HeartTestData.Commands.UpdateHeartTestData;
using SmartHealthSystem.Application.Features.HeartTestData.Queries.GetAllHeartTestResults;
using SmartHealthSystem.Application.Features.HeartTestData.Queries.GetHeartTestResultsByUserId;

namespace SmartHealthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HeartAnalysisController : ControllerBase
    {
        private readonly IMediator _mediator;

        public HeartAnalysisController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Yeni bir kalp analizi başlatır, Python AI servisine gider ve sonucu kaydeder.
        /// </summary>
        [HttpPost("create-analysis")]
        public async Task<IActionResult> CreateAnalysis([FromBody] CreateHeartTestDataCommandRequest request)
        {
            if (request == null)
                return BadRequest(new { message = "Geçersiz veri formu." });

            // MediatR sayesinde Handler otomatik tetiklenir
            var result = await _mediator.Send(request);

            if (result.IsSuccess)
            {
                return Ok(result);
            }

            return BadRequest(result);
        }
        [HttpGet("get-analyses/{patientId}")]
        public async Task<IActionResult> GetPatientAnalyses([FromRoute] string patientId)
        {
            // Mediator üzerinden Query Request'i gönderiyoruz
            var request = new GetHeartTestResultsByUserIdQueryRequest { UserId = patientId };
            var response = await _mediator.Send(request);

            // Eğer liste boşsa veya kullanıcı bulunamadıysa uygun mesajı dönebilirsin
            if (response == null || !response.Any())
                return NotFound(new { message = "Bu hastaya ait geçmiş analiz kaydı bulunamadı." });

            return Ok(response);
        }
        [HttpGet("all-technical-data")]
        public async Task<IActionResult> GetAllTechnicalData()
        {
            var response = await _mediator.Send(new GetAllHeartTestDataQueryRequest());
            return Ok(response);
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateHeartTestDataDto dto)
        {
            var response = await _mediator.Send(new UpdateHeartTestDataCommandRequest { UpdateHeartTestDataDto = dto });
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            // URL'den gelen ID'yi Request nesnesine bağlıyoruz
            var response = await _mediator.Send(new DeleteHeartTestDataCommandRequest { Id = id });
            if (response.IsSuccess)
                return Ok(response);

            return NotFound(response);
        }

    }
}

