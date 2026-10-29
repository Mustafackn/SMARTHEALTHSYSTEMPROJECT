using Microsoft.Extensions.FileProviders;
using SmartHealthSystem.Application;
using SmartHealthSystem.Infrastructure;
using SmartHealthSystem.Persistence;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddPersistenceServices(builder.Configuration);

// 1. ADIM: Servis kayıtları mutlaka Build'den ÖNCE yapılmalıdır
builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices();
builder.Services.AddControllersWithViews();
builder.Services.AddHttpClient();

// 2. ADIM: Uygulamayı burada oluşturuyoruz


var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}


app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.UseStaticFiles(); // wwwroot klasörünü dışarı açar
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(
        Path.Combine(builder.Environment.ContentRootPath, "wwwroot", "img", "banners")),
    RequestPath = "/img/banners"
});
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.Run();
