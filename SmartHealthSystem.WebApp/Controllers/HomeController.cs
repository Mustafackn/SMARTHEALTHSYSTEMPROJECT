using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.Features.AppUsers.Queries.GetUsers;
using SmartHealthSystem.WebApp.Models;
using System.Diagnostics;

namespace SmartHealthSystem.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IMediator _mediator;

        public HomeController(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task<IActionResult> Index()
        {
            // Handler doğrudan List<GetAllUsersDto> döndürdüğü için direkt değişkene atıyoruz
            var usersList = await _mediator.Send(new GetUsersQueryRequest());

            var model = new DashboardViewModel
            {
                // Liste sayısını doğrudan alıyoruz
                TotalPatientCount = usersList?.Count ?? 0,

                TotalAnalysisCount = 45, // Şimdilik manuel
                PendingResultsCount = 12,

                // Son kayıtları listeliyoruz. 
                // NOT: DTO'da CreatedDate olmadığı için tarihe göre sıralamayı şimdilik yapmıyoruz
                RecentAnalyses = usersList?
                    .Take(5) // İlk 5 kullanıcıyı al
                    .Select(u => new RecentAnalysisViewModel
                    {
                        // DTO'daki FirstName ve LastName'i birleştiriyoruz
                        PatientName = $"{u.FirstName} {u.LastName}",
                        PatientEmail = u.Email,
                        AnalysisType = "Genel Sağlık Taraması",
                        AnalysisDate = DateTime.Now, // DTO'da tarih olmadığı için şimdilik Now
                        ResultStatus = "Tamamlandı"
                    }).ToList() ?? new List<RecentAnalysisViewModel>(),

                Distribution = new AnalysisDistributionViewModel
                {
                    HeartAnalysisCount = 18,
                    KidneyAnalysisCount = 12,
                    CancerAnalysisCount = 10,
                    SocialActivityCount = 5
                }
            };

            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
