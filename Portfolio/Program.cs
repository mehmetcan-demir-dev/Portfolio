using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Context;
using Portfolio.DAL.Entities;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// **Veritaban� ba�lant�s�n� ekle**
builder.Services.AddDbContext<PortfolioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// **Identity Ayarlar�**
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PortfolioContext>()
    .AddDefaultTokenProviders();

// **Authentication & Authorization Ayarlar�**
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";   // Giri� sayfas�
    options.LogoutPath = "/Account/Logout"; // ��k�� sayfas�
    options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisiz giri� sayfas�
    options.SlidingExpiration = true; // Oturum s�resi dolmadan aktivite olursa uzat�l�r
});

// **Session Ayarlar�**
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dakika oturum s�resi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// **Lokalizasyon Ayarlar�**
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization() // View i�i lokalizasyon deste�i
    .AddDataAnnotationsLocalization(); // Model validasyonlar� i�in lokalizasyon

// **Desteklenen Diller**
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("tr-TR")
};

// **Lokalizasyon Middleware Ayarlar�**
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("tr-TR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// **Hata Y�netimi**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// **Middleware Kullan�mlar�**
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();  // **Kimlik do�rulama**
app.UseAuthorization();   // **Yetkilendirme**
app.UseSession();         // **Oturum Y�netimi**
app.UseRequestLocalization(); // **Lokalizasyon Middleware'i**

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
