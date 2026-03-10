using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.TestResultsData;
using SmartHealthSystem.Application.Features.TestResultsData.Query;

namespace SmartHealthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestResultApiController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TestResultApiController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Giriş yapan kullanıcının tüm geçmiş analizlerini getirir.
        /// </summary>
        /// <param name="userId">Kullanıcının Guid ID'si</param>
        [HttpGet("GetMyResults/{userId}")]
        public async Task<IActionResult> GetMyResults(Guid userId)
        {
            // Request nesnesini oluşturuyoruz
            var query = new GetPatientTestResultsQueryRequest { UserId = userId };

            // MediatR üzerinden Handler'a gönderiyoruz
            var response = await _mediator.Send(query);

            if (response.IsSuccess)
                return Ok(response);

            return NotFound(response);
        }
    }
}
