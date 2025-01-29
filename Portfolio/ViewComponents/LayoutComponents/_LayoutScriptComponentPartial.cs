using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents.LayoutComponents
{
	public class _LayoutScriptComponentPartial: ViewComponent
	{
		public IViewComponentResult Invoke()
		{
			return View();
		}
	}
}
