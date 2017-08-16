using System.Web.Mvc;
using TestWebApp.BLL;

namespace TestWebApp.Controllers
{
    public class BaseController: Controller
    {
        private AppUserManager _userManager;

        protected AppUserManager UserManager {
            get
            {
                if (_userManager != null) return _userManager;
                _userManager = new AppUserManager();

                return _userManager;
            }
        }
    }
}