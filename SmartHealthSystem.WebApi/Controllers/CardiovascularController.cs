using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.CreateCardiovascularTestData;
using SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.UpdateCardiovascularTestData;
using SmartHealthSystem.Application.Features.CardiovascularTestData.Queries.GetAllCardioTest;
using SmartHealthSystem.Application.Features.CardiovascularTestData.Queries.GetCardioResultsByUserId;

namespace SmartHealthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardiovascularController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CardiovascularController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Flutter'dan gelen verilerle kardiyovasküler risk analizi yapar ve kaydeder.
        /// </summary>
        [HttpPost("Analyze")]
        public async Task<IActionResult> AnalyzeCardio([FromBody] CreateCardiovascularTestDataCommandRequest request)
        {
            var response = await _mediator.Send(request);

            if (response.IsSuccess)
            {
                return Ok(response);
            }

            return BadRequest(response);
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAll()
        {
            var response = await _mediator.Send(new GetAllCardioTestDataQueryRequest());
            return Ok(response);
        }
        [HttpGet("GetByUserId/{userId}")]
        public async Task<IActionResult> GetByUserId(string userId)
        {
            var response = await _mediator.Send(new GetCardioResultsByUserIdQueryRequest { UserId = userId });
            return Ok(response);
        }

        [HttpPut("Update")]
        public async Task<IActionResult> Update([FromBody] UpdateCardiovascularTestDataCommandRequest request)
        {
            var response = await _mediator.Send(request);
            return response.IsSuccess ? Ok(response) : BadRequest(response);
        }
    }
}
