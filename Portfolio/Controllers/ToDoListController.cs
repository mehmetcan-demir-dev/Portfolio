using Microsoft.AspNetCore.Mvc;
using Portfolio.DAL.Context;
using Portfolio.DAL.Entities;

namespace Portfolio.Controllers
{
	public class ToDoListController : Controller
	{
		PortfolioContext context = new PortfolioContext();
		public IActionResult Index()
		{
			var values = context.ToDoLists.ToList();
			return View(values);
		}
		[HttpGet]
		public IActionResult CreateToDo()
		{
			return View();
		}
		[HttpPost]
		public IActionResult CreateToDo(ToDoList toDoList)
		{
			toDoList.Status = false;
			context.ToDoLists.Add(toDoList);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult DeleteToDo(int id)
		{
			var value = context.ToDoLists.Find(id);
			context.ToDoLists.Remove(value);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		[HttpGet]
		public IActionResult UpdateToDo(int id)
		{
			var value = context.ToDoLists.Find(id);
			return View(value);
		}
		[HttpPost]
		public IActionResult UpdateToDo(ToDoList toDoList)
		{
			context.ToDoLists.Update(toDoList);
			context.SaveChanges();
			return RedirectToAction("Index");
		}
		public IActionResult ChangeToDoStatusToTrue(int id)
		{
			var value = context.ToDoLists.Find(id);
			value.Status = true;
			context.SaveChanges();
			return RedirectToAction("Index");
		}
        public IActionResult ChangeToDoStatusToFalse(int id)
        {
            var value = context.ToDoLists.Find(id);
            value.Status = false;
            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
