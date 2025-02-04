using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Context;

namespace Portfolio.Controllers
{
    [Authorize]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Kullanıcının giriş yapıp yapmadığını kontrol et
            var userEmail = HttpContext.Session.GetString("UserEmail");
            if (string.IsNullOrEmpty(userEmail))
            {
                // Eğer kullanıcı giriş yapmamışsa login sayfasına yönlendir
                return RedirectToAction("Login", "Account");
            }

            ViewBag.UserEmail = userEmail; // Kullanıcı e-postasını View'e gönder

            var toDoList = _context.ToDoLists.ToList();
            var skills = _context.Skills
                     .OrderByDescending(s => s.Value)
                     .ThenBy(s => s.SkillID)
                     .Take(4)
                     .ToList();

            ViewBag.Skills = skills;
            return View(toDoList);
        }

        public IActionResult GetSkillsForDashboard()
        {
            var values = _context.Skills.ToList();
            return View(values);
        }
    }
}
