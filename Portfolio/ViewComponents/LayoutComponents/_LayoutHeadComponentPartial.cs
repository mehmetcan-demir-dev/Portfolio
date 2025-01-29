using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents.LayoutComponents
{
	public class _LayoutHeadComponentPartial : ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
