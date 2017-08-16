using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using TestWebApp.ViewModels;
using System.ComponentModel.DataAnnotations;

namespace TestWebApp.Controllers
{
    public class AccountController: BaseController
    {
        #region Login 

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login() {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
               return await TrySignIn(model.Email, model.Password);

            throw new ValidationException("Please enter all required data to login!");
        }

        #endregion

        #region registration

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel user)
        {
            if (ModelState.IsValid)
            {
                await UserManager.Add(user);
                return RedirectToAction("Login");
            }

            throw new ValidationException("Registraion model is wrong!");
        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }

        #endregion

        #region Logout

        [HttpGet]
        public ActionResult Logout()
        {
            //AuthManager.SignOut();
            return RedirectToAction("Login");
        }

        private async Task<ActionResult> TrySignIn(string emailOrUserName, string password)
        {
            var sigInResult = UserManager.SignInUser(emailOrUserName, password);
            if (sigInResult.Success)
                return Redirect(Url.Action("Index", "Home"));

            throw new AggregateException(sigInResult.Exceptions);
        }

        #endregion
    }
}