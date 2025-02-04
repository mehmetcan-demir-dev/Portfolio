using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Context;
using Portfolio.DAL.Entities;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// **Veritabaný baðlantýsýný ekle**
builder.Services.AddDbContext<PortfolioContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// **Identity Ayarlarý**
builder.Services.AddIdentity<User, IdentityRole>()
    .AddEntityFrameworkStores<PortfolioContext>()
    .AddDefaultTokenProviders();

// **Authentication & Authorization Ayarlarý**
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";   // Giriþ sayfasý
    options.LogoutPath = "/Account/Logout"; // Çýkýþ sayfasý
    options.AccessDeniedPath = "/Account/AccessDenied"; // Yetkisiz giriþ sayfasý
    options.SlidingExpiration = true; // Oturum süresi dolmadan aktivite olursa uzatýlýr
});

// **Session Ayarlarý**
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30); // 30 dakika oturum süresi
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// **Lokalizasyon Ayarlarý**
builder.Services.AddLocalization(options => options.ResourcesPath = "Resources");

builder.Services.AddControllersWithViews()
    .AddViewLocalization() // View içi lokalizasyon desteði
    .AddDataAnnotationsLocalization(); // Model validasyonlarý için lokalizasyon

// **Desteklenen Diller**
var supportedCultures = new[]
{
    new CultureInfo("en-US"),
    new CultureInfo("tr-TR")
};

// **Lokalizasyon Middleware Ayarlarý**
builder.Services.Configure<RequestLocalizationOptions>(options =>
{
    options.DefaultRequestCulture = new RequestCulture("tr-TR");
    options.SupportedCultures = supportedCultures;
    options.SupportedUICultures = supportedCultures;
});

var app = builder.Build();

// **Hata Yönetimi**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

// **Middleware Kullanýmlarý**
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();  // **Kimlik doðrulama**
app.UseAuthorization();   // **Yetkilendirme**
app.UseSession();         // **Oturum Yönetimi**
app.UseRequestLocalization(); // **Lokalizasyon Middleware'i**

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
