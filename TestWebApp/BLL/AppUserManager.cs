using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Authentication;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using TestWebApp.DAL;
using TestWebApp.Mappings;
using TestWebApp.Models;
using TestWebApp.ViewModels;

namespace TestWebApp.BLL
{
    public class AppUserManager: UserManager<AppUser>
    {
        #region properties

        public AppUserManager(IUserStore<AppUser> store) : base(store) {}

        private static IAuthenticationManager _authManager;
        private static IMapper _mapper;

        #endregion

        #region App User Manager creation

        public static AppUserManager Create(IdentityFactoryOptions<AppUserManager> options, IOwinContext context)
        {
            var manager = new AppUserManager(new UserStore<AppUser>(context.Get<TestAppContext>()));

            var config = new AutoMapperConfiguration().Configure();
            _mapper = config.CreateMapper();
            _authManager = context.Authentication;

            return manager;
        }

        #endregion

        #region Custom logic

        public async Task Add(RegisterViewModel user)
        {
            await CreateAsync(_mapper.Map(user, new AppUser()), user.Password);
        }

        public List<UserManageModel> GetUsers()
        {
            return _mapper.Map(Users.OrderBy(u => u.UserName).ToList(), new List<UserManageModel>());
        }

        public UserManageModel GetUser(string userId)
        {
            return _mapper.Map(GetIfExists(u => u.Id.Equals(userId)), new UserManageModel());
        }

        public async Task<UserManageModel> Edit(UserManageModel userView)
        {
            var existingUser = GetIfExists(u => u.UserName.Equals(userView.UserName));

            _mapper.Map(userView, existingUser);
            await UpdateAsync(existingUser);

            return userView;
        }

        public async Task<SignInResult> SignInUser(string emailOrUserName, string password)
        {
            var signInResult = new SignInResult();

            var existingUser = await Users.SingleOrDefaultAsync(u => u.Email.ToLower().Equals(emailOrUserName.ToLower()) || u.UserName.ToLower().Equals(emailOrUserName.ToLower()));
            if (existingUser != null) {
                var passwordsMatch = (new PasswordHasher()).VerifyHashedPassword(existingUser.PasswordHash, password);

                if (passwordsMatch == PasswordVerificationResult.Failed)
                    signInResult.Exceptions.Add(new AuthenticationException("The password is wrong!"));

                var ident = this.CreateIdentity(existingUser, DefaultAuthenticationTypes.ApplicationCookie);
                _authManager.SignIn(new AuthenticationProperties { IsPersistent = false }, ident);
            }
            else
                signInResult.Exceptions.Add(new AuthenticationException("User with such credentials doesn't exist!"));

            return signInResult;
        }

        private AppUser GetIfExists(Expression<System.Func<AppUser, bool>> exp) {

            var existedUser = Users.SingleOrDefault(exp);
            if (existedUser == null)
                throw new KeyNotFoundException("There is no user for such condition!");

            return existedUser;
        }

        #endregion
    }
}