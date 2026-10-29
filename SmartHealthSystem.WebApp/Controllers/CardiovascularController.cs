using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers;
using SmartHealthSystem.Application.Features.CardiovascularTestData.Commands.CreateCardiovascularTestData;

namespace SmartHealthSystem.WebApp.Controllers
{
    public class CardiovascularController : Controller
    {
        private readonly IMediator _mediator;

        public CardiovascularController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // Analiz Formunu Açan Metot
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            // 1. Tüm kullanıcıları (User rolündekileri) çekiyoruz
            var users = await _mediator.Send(new GetUsersQueryRequest());

            // 2. Kullanıcı listesini View'a gönderiyoruz
            ViewBag.UserList = users;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCardiovascularTestDataCommandRequest request)
        {
            if (ModelState.IsValid)
            {
                var response = await _mediator.Send(request);

                if (response.IsSuccess)
                {
                    TempData["RiskPercentage"] = response.RiskPercentage.ToString();
                    TempData["HealthScore"] = response.HealthScore.ToString();
                    TempData["Recommendations"] = response.Recommendations;
                    TempData["SuccessMessage"] = response.Message;

                    return RedirectToAction(nameof(Result));
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            // Hata durumunda listeyi tekrar doldurmamız lazım ki sayfa boş kalmasın
            ViewBag.UserList = await _mediator.Send(new GetUsersQueryRequest());
            return View(request);
        }
        [HttpGet]
        public IActionResult Result()
        {
            var riskStr = TempData.Peek("RiskPercentage")?.ToString();

            if (string.IsNullOrEmpty(riskStr))
            {
                return RedirectToAction(nameof(Create));
            }

            // View'a gönderirken string olarak gönderelim, View içinde sayıya çevireceğiz
            ViewBag.RiskPercentage = riskStr;
            ViewBag.HealthScore = TempData.Peek("HealthScore")?.ToString();
            ViewBag.Recommendations = TempData.Peek("Recommendations");

            return View();
        }
    }
}
