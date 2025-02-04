using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Portfolio.DAL.Context;
using Portfolio.DAL.Entities;

namespace Portfolio.Controllers
{
    [Authorize]
    public class DocumentController : Controller
    {
        PortfolioContext context = new PortfolioContext();

        public IActionResult DocumentList()
        {
            var values = context.Documents.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateDocument()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateDocument(Document document)
        {
            context.Documents.Add(document);
            context.SaveChanges();
            return RedirectToAction("DocumentList");
        }
        [HttpGet]
        public IActionResult UpdateDocument(int id)
        {
            var value = context.Documents.Find(id);
            return View(value);
        }
        [HttpPost]
        public IActionResult UpdateDocument(Document document)
        {
            context.Documents.Update(document);
            context.SaveChanges();
            return RedirectToAction("DocumentList");
        }
    }
}
