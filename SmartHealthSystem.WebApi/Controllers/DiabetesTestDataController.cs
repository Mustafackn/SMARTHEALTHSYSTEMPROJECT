using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.DiabetesTestData.Commands.CreateDiabetesTestData;
using SmartHealthSystem.Application.Features.DiabetesTestData.Commands.DeleteDiabetesTestData;
using SmartHealthSystem.Application.Features.DiabetesTestData.Commands.UpdateDiabetesTestData;
using SmartHealthSystem.Application.Features.DiabetesTestData.Queries.GetAllDiabetesTest;
using SmartHealthSystem.Application.Features.DiabetesTestData.Queries.GetDiabetesResultsByUserIdQuery;

namespace SmartHealthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DiabetesTestDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DiabetesTestDataController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Yeni bir diyabet analizi başlatır ve AI sonucunu kaydeder.
        /// </summary>
        [HttpPost("CreateAnalysis")]
        public async Task<IActionResult> Create([FromBody] CreateDiabetesTestDataCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }
        [HttpGet("GetAllResults")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllDiabetesTestDataQueryRequest());

            // Veri boş olsa bile boş liste döner, 200 OK uygundur.
            return Ok(response);
        }
        [HttpGet("GetResultsByUserId/{userId}")]
        public async Task<IActionResult> GetResultsByUserId(string userId)
        {
            var response = await _mediator.Send(new GetDiabetesResultsByUserIdQueryRequest { UserId = userId });
            return Ok(response);
        }
        [HttpPut("UpdateAnalysis")]
        public async Task<IActionResult> Update([FromBody] UpdateDiabetesTestDataCommandRequest request)
        {
            var response = await _mediator.Send(request);
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response);
        }
        [HttpDelete("DeleteAnalysis/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _mediator.Send(new DeleteDiabetesTestDataCommandRequest { Id = id });
            if (response.IsSuccess) return Ok(response);
            return BadRequest(response);
        }
    }
}
