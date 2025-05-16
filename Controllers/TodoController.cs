using Microsoft.AspNetCore.Mvc;
using TodoApp.Models;
using TodoApp.Services;
using System.Linq;

namespace TodoApp.Controllers
{
    public class TodoController : Controller
    {
        private readonly TodoService _service;

        public TodoController(TodoService service)
        {
            _service = service;
        }

        public IActionResult Index(string category, Priority? priority)
        {
            var todos = _service.GetAll(category, priority);
            ViewBag.Categories = _service.GetCategories();
            return View(todos);
        }

        [HttpPost]
        public IActionResult Add(string title, string category, Priority priority)
        {
            _service.Add(title, category, priority);
            return RedirectToAction("Index");
        }

        public IActionResult MarkAsComplete(int id)
        {
            _service.MarkAsComplete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Hide(int id)
        {
            _service.Hide(id);
            return RedirectToAction("Index");
        }
    }
}
