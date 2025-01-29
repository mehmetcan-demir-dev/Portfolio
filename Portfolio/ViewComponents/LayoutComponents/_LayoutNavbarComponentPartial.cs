using Microsoft.AspNetCore.Mvc;
using Portfolio.DAL.Context;

namespace Portfolio.ViewComponents.LayoutComponents
{
	public class _LayoutNavbarComponentPartial : ViewComponent
	{
		PortfolioContext context = new PortfolioContext();
        public IViewComponentResult Invoke()
        {
            ViewBag.toDoCount = context.Messages.Where(x => !x.IsRead).Count();
            var values1 = context.Messages.Where(x => !x.IsRead).ToList();
            return View(values1);
        }

    }
}
