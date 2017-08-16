using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.AspNet.Identity.Owin;
using TestWebApp.BLL;
using TestWebApp.ViewModels;

namespace TestWebApp.Controllers.ApiControllers
{
    [RoutePrefix("api/users")]
    public class UsersController: ApiController
    {
        private AppUserManager _appUserManager;
        private AppUserManager UserManager => _appUserManager ?? Request.GetOwinContext().GetUserManager<AppUserManager>();

        [HttpPost]
        [Route("add")]
        public async Task Add([FromBody]RegisterViewModel user)
        {
            if (ModelState.IsValid)
                await UserManager.Add(user);
            else
                throw new ValidationException("User model is not valid!");
        }

        [HttpPost]
        [Route("edit")]
        public async Task Edit([FromBody]UserManageModel user)
        {
            if (ModelState.IsValid)
                await UserManager.Edit(user);
            else
                throw new ValidationException("User model is not valid!");
        }

        public List<UserManageModel> Get()
        {
            return UserManager.GetUsers();
        }
    }
}