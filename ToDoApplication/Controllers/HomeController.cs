using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Areas.Identity.Data;
using ToDoApplication.Data;
using ToDoApplication.Models;

namespace ToDoApplication.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly UserManager<ToDoApplicationUser> _userManager;
        private readonly ToDoContext cn;

        public IList<Task>? UserTasks { get; set; }

        public HomeController(UserManager<ToDoApplicationUser> userManager, ToDoContext cn)
        {
            _userManager = userManager;
            this.cn = cn;
        }

        public IActionResult Index(int Id)
        {
            var userId = ViewData["UserId"] = _userManager.GetUserId(this.User);
            IQueryable<ToDoModel> Query = cn.Todos.Where(x => x.UserId == userId).OrderByDescending(t => t.Status);

            return View(Query.ToList());
        }

        [HttpGet]
        public IActionResult Add()
        {
            ViewData["UserId"] = _userManager.GetUserId(this.User);
            var task = new ToDoModel();
            return View(task);
        }

        [HttpPost]
        public IActionResult Add(ToDoModel Task)
        {
            if (ModelState.IsValid)
            {
                cn.Todos.Add(Task);
                cn.SaveChanges();
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult MarkAsComplete([FromRoute] int ID, ToDoModel CompletedTask)
        {
            CompletedTask = cn.Todos.SingleOrDefault(t => t.Id == CompletedTask.Id && t.UserId == _userManager.GetUserId(this.User))!;
            if (CompletedTask != null)
            {
                CompletedTask.Status = "Done";
                cn.SaveChanges();
            }
            return RedirectToAction("Index", new { Id = ID });
        }

        [HttpPost]
        public IActionResult DeleteCompletedTasks(int Id)
        {
            var ToDelete = cn.Todos.Where(t => t.Status == "Done" && t.UserId == _userManager.GetUserId(this.User));
            foreach (var item in ToDelete)
            {
                cn.Remove(item);
            }
            cn.SaveChanges();
            return RedirectToAction("Index", new { ID = Id });
        }
    }
}