using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Portfolio.DAL.Entities;
using Portfolio.Models;

namespace Portfolio.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Login(string returnUrl = null)
        {
            if (User?.Identity?.IsAuthenticated == true)
            {
                return RedirectToAction("Index", "Dashboard");
            }

            // Login sayfasında returnUrl parametresini ViewData'ya doğru şekilde aktar
            ViewData["ReturnUrl"] = returnUrl ?? Url.Content("~/");  // Varsayılan olarak anasayfaya yönlendir
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        // **Session'a kullanıcı bilgisini kaydet**
                        HttpContext.Session.SetString("UserEmail", user.Email);
                        HttpContext.Session.SetString("UserId", user.Id);

                        // Eğer returnUrl varsa, kullanıcıyı oraya yönlendir
                        return Redirect(returnUrl ?? "/Dashboard/Index"); // Varsayılan olarak Dashboard/Index
                    }
                    else if (result.IsLockedOut)
                    {
                        return View("Lockout");
                    }
                }

                ModelState.AddModelError(string.Empty, "E-posta veya şifre hatalı.");
            }

            return View(model);
        }

        // Kullanıcı kayıt işlemi
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Dashboard");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // **Session'ı temizle**
            HttpContext.Session.Clear();

            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync();

            return RedirectToAction("Login", "Account");
        }
    }
}
