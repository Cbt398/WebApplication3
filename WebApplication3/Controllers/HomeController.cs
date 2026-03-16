using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using WebApplication3.Models;

namespace WebApplication3.Controllers
{
    public class HomeController : Controller
    {
        private string _connectionString = @"Data Source=.\sqlexpress;Initial Catalog=ToDo;Integrated Security=true;TrustServerCertificate=true;";

        public IActionResult Index()
        {
            ToDoManager mgr = new ToDoManager(_connectionString);
            ToDoViewModel vm = new ToDoViewModel();
            vm.ToDo = mgr.GetItems(false);
            return View(vm);
        }
        public IActionResult CompletedTask()
        {
            ToDoManager mgr = new ToDoManager(_connectionString);
            ToDoViewModel vm = new ToDoViewModel();
            vm.ToDo = mgr.GetItems(true);
            return View(vm);
        }
        public IActionResult ActiveTasks()
        {
            ToDoManager mgr = new ToDoManager(_connectionString);
            ToDoViewModel vm = new ToDoViewModel();
            vm.ToDo = mgr.GetItems(false);
            return View("Index", vm);

        }
        public IActionResult Categories()
        {
            ToDoManager mgr = new ToDoManager(_connectionString);
            ToDoViewModel vm = new ToDoViewModel();
            vm.Categories = mgr.GetCategories();
            return View(vm);
        }
        public IActionResult AddToDo()
        {
            ToDoManager mgr = new ToDoManager(_connectionString);
            ToDoViewModel vm = new ToDoViewModel();
            vm.Categories = mgr.GetCategories();
            return View(vm);
        }

        [HttpPost]
        public IActionResult AddToDo(ToDoItems toDo)
        {
            ToDoManager mgr = new ToDoManager(_connectionString);
            mgr.AddItem(toDo);
            return Redirect("/Home/Index");
        }

        public IActionResult AddCategory(ToDoItems todo)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            ToDoManager mgr = new ToDoManager(_connectionString);
            mgr.AddCategory(category);
            return Redirect("/Home/Index");
        }
        public IActionResult EditCategory(int id)
        {
            ToDoManager mgr = new ToDoManager(_connectionString);

            return View(new ToDoViewModel
            {
                Category = mgr.GetCategoryById(id)
            });
        }

        [HttpPost]
        public IActionResult EditCategory(Category category)
        {
            ToDoManager mgr = new ToDoManager(_connectionString);
            Console.WriteLine($"Name: {category.Name}, Id: {category.Id}");

            mgr.EditCategory(category);
            return Redirect("/Home/Categories");
        }
        [HttpPost]
        public IActionResult MarkCompleted(int id)
        {
            ToDoManager mgr = new ToDoManager(_connectionString);
            mgr.CompleteToDo(id);
            return Redirect("/Home/Index");
        }

    }
}
