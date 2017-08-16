using System.Threading.Tasks;
using System.Web.Mvc;
using TestWebApp.ViewModels;

namespace TestWebApp.Controllers
{
    [Authorize]
    public class UserController: BaseController
    {
        [HttpGet]
        public ActionResult Edit(int id)
        {
            return View(UserManager.GetUser(id));
        }

        [HttpPost]
        public async Task<ActionResult> Edit(UserManageModel user) 
        {
            if (ModelState.IsValid)
            {
                await UserManager.Edit(user);
                return RedirectToAction("List");
            }
            return View();
        }

        [HttpGet]
        public ActionResult List()
        {
            var users = UserManager.GetUsers();
            return View("List", users);
        }

        [HttpGet]
        public ActionResult ListApi()
        {
            return View("~/Views/Api/UserList.cshtml");
        }
    }
}