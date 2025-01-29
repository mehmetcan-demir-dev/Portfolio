using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.DAL.Context;

namespace Portfolio.Controllers
{
    public class DashboardController : Controller
    {
        PortfolioContext context = new PortfolioContext();

        public IActionResult Index()
        {
            var toDoList = context.ToDoLists.ToList();
            var skills = context.Skills
                     .OrderByDescending(s => s.Value) // Önce Value'ya göre azalan sıralama yap
                     .ThenBy(s => s.SkillID) // Aynı Value'ya sahip olanları ID'ye göre artan sıralama yap
                     .Take(4) // İlk 4 tanesini al
                     .ToList();
            ViewBag.Skills = skills;
            return View(toDoList);
        }
        public IActionResult GetSkillsForDashboard()
        {
            var values = context.Skills.ToList();
            return View(values);
        }

    }
}
