using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;
using TestWebApp.BLL;
using TestWebApp.DAL;

namespace TestWebApp
{
    public class IdentityConfig
    {
        public void Configuration(IAppBuilder app) {
            app.CreatePerOwinContext(() => new TestAppContext());
            app.CreatePerOwinContext<AppUserManager>(AppUserManager.Create);

            app.UseCookieAuthentication(new CookieAuthenticationOptions {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
            });
        }
    }
}