using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using TestWebApp.BLL;
using TestWebApp.DAL;

namespace TestWebApp.Controllers
{
    public class BaseController: Controller
    {
        private AppUserManager _userManager;
        private IAuthenticationManager _authManager;
        private TestAppContext _context;

        protected TestAppContext Context {
            get
            {
                if (_context != null) return _context;
                _context = HttpContext.GetOwinContext().Get<TestAppContext>();

                return _context;
            }
        }

        protected AppUserManager UserManager {
            get
            {
                if (_userManager != null) return _userManager;
                _userManager = HttpContext.GetOwinContext().GetUserManager<AppUserManager>();

                return _userManager;
            }
        }

        protected IAuthenticationManager AuthManager {
            get
            {
                if (_authManager != null) return _authManager;
                _authManager = HttpContext.GetOwinContext().Authentication;

                return _authManager;
            }
        }
    }
}