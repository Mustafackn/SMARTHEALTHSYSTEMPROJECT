using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers;
using SmartHealthSystem.Application.Features.DiabetesTestData.Commands.CreateDiabetesTestData;

namespace SmartHealthSystem.WebApp.Models
{
    public class DiabetesController : Controller
    {
        private readonly IMediator _mediator;

        public DiabetesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // 1. Analiz Formunu Açan Metot (GET)
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // Sisteme kayıtlı hastaları çekiyoruz (Dropdown/Seçim listesi için)
            var users = await _mediator.Send(new GetUsersQueryRequest());
            ViewBag.UserList = users;

            return View();
        }

        // 2. Analizi Başlatan Metot (POST)
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateDiabetesTestDataCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                // MediatR üzerinden Handler'a (ve dolayısıyla Python API'ye) gidiyoruz
                var response = await _mediator.Send(request);

                if (response.IsSuccess)
                {
                    // Başarılı sonucu Result sayfasına taşımak için TempData kullanıyoruz
                    TempData["RiskPercentage"] = response.RiskPercentage.ToString("F2");
                    TempData["HealthScore"] = response.HealthScore.ToString();
                    TempData["Recommendations"] = response.Recommendations;
                    TempData["SuccessMessage"] = response.Message;

                    return RedirectToAction(nameof(Result));
                }

                // API'den veya Handler'dan hata gelirse ekrana basıyoruz
                ModelState.AddModelError(string.Empty, response.Message);
            }

            // Hata durumunda (Validation hatası dahil) kullanıcı listesini tekrar doldurmalıyız
            ViewBag.UserList = await _mediator.Send(new GetUsersQueryRequest());
            return View(request);
        }

        // 3. Analiz Sonucunu Gösteren Metot (GET)
        [HttpGet]
        public IActionResult Result()
        {
            // TempData.Peek() kullanarak veriyi okuyoruz (Sayfa yenilense de veri hemen silinmez)
            var riskStr = TempData.Peek("RiskPercentage")?.ToString();

            if (string.IsNullOrEmpty(riskStr))
            {
                return RedirectToAction(nameof(Create));
            }

            ViewBag.RiskPercentage = riskStr;
            ViewBag.HealthScore = TempData.Peek("HealthScore")?.ToString();
            ViewBag.Recommendations = TempData.Peek("Recommendations");

            return View();
        }
    }
}

