using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.DAL.Context;

namespace Portfolio.Controllers
{
    public class StatisticController : Controller
    {
        PortfolioContext context = new PortfolioContext();
        public IActionResult Index()
        {
            ViewBag.s1 = context.Skills.Count();
            ViewBag.s2 = context.Messages.Count();
            ViewBag.s3 = context.Messages.Where(x=>!x.IsRead).Count();
            ViewBag.s4 = context.Messages.Where(x=>x.IsRead).Count();
            return View();
        }
    }
}
