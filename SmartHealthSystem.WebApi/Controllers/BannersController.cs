using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.Banners.Queries.GetAllBanners;

namespace SmartHealthSystem.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BannersController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BannersController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/banners
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var query = new GetAllBannersQueryRequest();
            var response = await _mediator.Send(query);

            if (response.Succeeded)
            {
                // Flutter emülatörü bilgisayarı "10.0.2.2" IP'si ile görür.
                // WebApi projenin çalıştığı portu (Örn: 5096) buraya yazmalısın.
                var apiBaseUrl = "http://10.0.2.2:5096";

                foreach (var banner in response.Banners)
                {
                    if (!string.IsNullOrEmpty(banner.ImageUrl))
                    {
                        // 1. Veritabanından gelen yolu temizle (başındaki / işaretini at)
                        var cleanPath = banner.ImageUrl.TrimStart('/');

                        // 2. Resim yolunu WebApi üzerinden servis ettiğimiz sanal klasöre yönlendir.
                        // Hatırlatma: Program.cs içinde RequestPath = "/webapp_files" yapmıştık.
                        banner.ImageUrl = $"{apiBaseUrl}/webapp_files/{cleanPath}";
                    }
                }
                return Ok(response);
            }
            return BadRequest(response);
        }

    }
}
