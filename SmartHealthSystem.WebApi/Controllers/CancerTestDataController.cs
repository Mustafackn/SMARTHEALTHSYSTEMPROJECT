using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.CancerTestData.Commands.CreateCancerTestData;
using SmartHealthSystem.Application.Features.CancerTestData.Commands.DeleteCancerTestData;
using SmartHealthSystem.Application.Features.CancerTestData.Commands.UpdateCancerTestData;
using SmartHealthSystem.Application.Features.CancerTestData.Queries.GetAllCancerTestResults;
using SmartHealthSystem.Application.Features.CancerTestData.Queries.GetCancerTestResultsByUserIdQuery;

namespace SmartHealthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CancerTestDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CancerTestDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Meme Kanseri Analizi Başlatır
        /// </summary>
        /// <param name="request">Hücre çekirdeği ölçüm verileri (Radius, Texture, Area vb.)</param>
        /// <returns>Python AI servisinden gelen %95.61 doğruluktaki analiz sonucu</returns>
        [HttpPost("CreateAnalysis")]
        public async Task<IActionResult> CreateAnalysis([FromBody] CreateCancerTestDataCommandRequest request)
        {
            // MediatR sayesinde doğrudan Handler'a gider
            var response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        // <summary>
        /// Tüm meme kanseri analiz geçmişini getirir.
        /// </summary>
        /// <returns>List of GetAllCancerTestDataQueryResponse</returns>
        [HttpGet("getall")]
        public async Task<ActionResult<List<GetAllCancerTestDataQueryResponse>>> GetAll()
        {
            // MediatR üzerinden Handler'a isteği gönderiyoruz
            var response = await _mediator.Send(new GetAllCancerTestDataQueryRequest());

            if (response == null || response.Count == 0)
            {
                return NotFound("Kayıtlı analiz verisi bulunamadı.");
            }

            return Ok(response);
        }
        [HttpGet("user-history/{userId}")]
        public async Task<IActionResult> GetUserHistory(string userId)
        {
            // Daha önce yazdığımız GetCancerTestResultsByUserIdQueryRequest'i tetikler
            var query = new GetCancerTestResultsByUserIdQueryRequest { UserId = userId };
            return Ok(await _mediator.Send(query));
        }
        [HttpPut("update")]
        public async Task<IActionResult> Update([FromBody] UpdateCancerTestDataCommandRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response);
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteCancerTestDataCommandRequest { Id = id });
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response);
        }

    }
}
