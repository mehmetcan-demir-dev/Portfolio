using Microsoft.AspNetCore.Mvc;
using Portfolio.DAL.Context;

namespace Portfolio.ViewComponents
{
    public class _StatisticsComponentPartial : ViewComponent
    {
        //dinamik sekle getirmek icin veritabaninda bir tablo daha acman gerek adı da Statistics olacak.
        //PortfolioContext portfolioContext = new PortfolioContext();
        //public IViewComponentResult Invoke()
        //{
        //    var values = portfolioContext.Statistics.ToList();
        //    return View(values);
        //}
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
