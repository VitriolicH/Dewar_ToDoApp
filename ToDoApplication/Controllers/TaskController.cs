using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ToDoApplication.Areas.Identity.Data;
using ToDoApplication.Data;
using ToDoApplication.Models;

namespace ToDoApplication.Controllers
{
    [Authorize]
    public class TaskController : Controller
    {
        private readonly UserManager<ToDoApplicationUser> _userManager;
        private readonly ToDoContext cn;

        public TaskController(UserManager<ToDoApplicationUser> userManager, ToDoContext cn)
        {
            _userManager = userManager;
            this.cn = cn;
        }


        public IActionResult Index(int id)
        {
            ToDoModel model = cn.Todos.FirstOrDefault(x => x.Id == id)!;
            return View("Edit", model);
        }

        [HttpPost]
        public IActionResult Edit(ToDoModel modelToUpdate)
        {
            ToDoModel CurentModel = cn.Todos.Find(modelToUpdate.Id)!;
            if (CurentModel != null)
            {
                CurentModel.TaskName = modelToUpdate.TaskName;
                CurentModel.Description = modelToUpdate.Description;
                CurentModel.DueDate = modelToUpdate.DueDate;
                CurentModel.Status = modelToUpdate.Status;

                cn.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        
    }
}
