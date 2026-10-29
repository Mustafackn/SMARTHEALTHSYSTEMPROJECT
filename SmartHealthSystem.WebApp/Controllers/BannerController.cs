using MediatR;
using Microsoft.AspNetCore.Mvc;
using SmartHealthSystem.Application.DTOs.BannerDto;
using SmartHealthSystem.Application.Features.Banners.Commands.CreateBanner;
using SmartHealthSystem.Application.Features.Banners.Commands.UpdateBanner;
using SmartHealthSystem.Application.Features.Banners.Queries.GetAllBanners;
using SmartHealthSystem.Application.Features.Banners.Queries.GetBannerById;

namespace SmartHealthSystem.WebApp.Controllers
{
    public class BannerController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IWebHostEnvironment _webHostEnvironment; // Klasör yolu için eklendi

        public BannerController(IMediator mediator, IWebHostEnvironment webHostEnvironment)
        {
            _mediator = mediator;
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<IActionResult> Index()
        {
            // Query'yi oluşturuyoruz
            var query = new GetAllBannersQueryRequest();

            // MediatR ile Handler'a gönderiyoruz
            var response = await _mediator.Send(query);

            // Eğer başarılıysa listeyi View'a gönder, değilse boş liste gönder (hata almamak için)
            return View(response.Banners ?? new List<GetBannerDto>());
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBannerDto bannerDto)
        {
            if (ModelState.IsValid)
            {
                // 1. Bilgisayardan seçilen bir dosya var mı kontrol et
                if (bannerDto.ImageFile != null)
                {
                    // Dosya adını benzersiz yap (Örn: unique-id.jpg)
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(bannerDto.ImageFile.FileName);

                    // Kaydedilecek klasörü belirle: wwwroot/img/banners
                    string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img", "banners");

                    // Klasör yoksa oluştur
                    if (!Directory.Exists(uploadDir))
                        Directory.CreateDirectory(uploadDir);

                    string filePath = Path.Combine(uploadDir, fileName);

                    // Dosyayı fiziksel olarak kaydet
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await bannerDto.ImageFile.CopyToAsync(fileStream);
                    }

                    // DB'ye kaydedilecek olan string yolu DTO'ya ata
                    bannerDto.ImageUrl = "/img/banners/" + fileName;
                }

                // 2. MediatR Command'ı gönder
                var response = await _mediator.Send(new CreateBannerCommandRequest { Banner = bannerDto });

                if (response.Succeeded)
                {
                    TempData["SuccessMessage"] = response.Message;
                    return RedirectToAction(nameof(Index));
                }

                ModelState.AddModelError(string.Empty, response.Message);
            }

            return View(bannerDto);
        }
        [HttpGet]
public async Task<IActionResult> Update(Guid id)
{
    // 1. Güncellenecek veriyi GetById Query ile getiriyoruz
    // Eğer bu query henüz yoksa, repository üzerinden de çekebilirsin.
    var response = await _mediator.Send(new GetBannerByIdQueryRequest { Id = id });

    if (response.Banner == null)
    {
        return NotFound();
    }

    // 2. Veriyi DTO'ya map ederek View'a gönderiyoruz
    var updateDto = new UpdateBannerDto
    {
        Id = response.Banner.Id,
        Title = response.Banner.Title,
        ImageUrl = response.Banner.ImageUrl,
        TargetUrl = response.Banner.TargetUrl,
        IsActive = response.Banner.IsActive,
        Order = response.Banner.Order
        // Varsa Description vb. alanları da ekle
    };

    return View(updateDto);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Update(UpdateBannerDto updateBannerDto)
{
    if (ModelState.IsValid)
    {
        // 1. Yeni bir resim yüklenmiş mi kontrol et
        if (updateBannerDto.ImageFile != null)
        {
            // Yeni dosya adını oluştur
            string fileName = Guid.NewGuid().ToString() + Path.GetExtension(updateBannerDto.ImageFile.FileName);
            string uploadDir = Path.Combine(_webHostEnvironment.WebRootPath, "img", "banners");

            // Klasör kontrolü
            if (!Directory.Exists(uploadDir))
                Directory.CreateDirectory(uploadDir);

            string filePath = Path.Combine(uploadDir, fileName);

            // Yeni dosyayı kaydet
            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await updateBannerDto.ImageFile.CopyToAsync(fileStream);
            }

            // Opsiyonel: Eski resmi klasörden silmek istersen burada silebilirsin.
            // if (!string.IsNullOrEmpty(updateBannerDto.ImageUrl)) { ... }

            // Yeni yolu DTO'ya ata
            updateBannerDto.ImageUrl = "/img/banners/" + fileName;
        }

        // 2. MediatR Command'ı gönder
        var response = await _mediator.Send(new UpdateBannerCommandRequest { UpdateBannerDto = updateBannerDto });

        if (response.Succeeded)
        {
            TempData["SuccessMessage"] = response.Message;
            return RedirectToAction(nameof(Index));
        }

        ModelState.AddModelError(string.Empty, response.Message);
    }

    return View(updateBannerDto);
}
    
    }
}
