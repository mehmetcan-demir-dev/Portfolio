using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.DAL.Context;
using Portfolio.Models; // ViewModel'in olduğu namespace'i ekleyin

namespace Portfolio.Controllers
{
    public class FeatureController : Controller
    {       
        public IActionResult Index()
        {
            return View();
        }
    }
}
