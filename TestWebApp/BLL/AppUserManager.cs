using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AutoMapper;
using TestWebApp.DAL;
using TestWebApp.Mappings;
using TestWebApp.Models;
using TestWebApp.ViewModels;

namespace TestWebApp.BLL
{
    public class AppUserManager
    {
        #region properties

        private static TestAppContext _db;
        private readonly PasswordManager _pwdManager = new PasswordManager();
        private static IMapper _mapper;

        public AppUserManager()
        {
            _db = new TestAppContext();
            var config = new AutoMapperConfiguration().Configure();
            _mapper = config.CreateMapper();
        }
        #endregion

        #region Custom logic

        public async Task<UserManageModel> Add(RegisterViewModel user)
        {
            var appUser = _mapper.Map(user, new AppUser());
            HashUserPassword(appUser);

            var savedUser = _db.Users.Add(appUser);
            await _db.SaveChangesAsync();

            return _mapper.Map(savedUser, new UserManageModel());
        }

        public List<UserManageModel> GetUsers()
        {
            return _mapper.Map(_db.Users.ToList(), new List<UserManageModel>());
        }

        public UserManageModel GetUser(int userId)
        {
            return _mapper.Map(GetIfExists(u => u.Id.Equals(userId)), new UserManageModel());
        }

        public async Task<UserManageModel> Edit(UserManageModel userView)
        {
            var existingUser = await GetIfExists(u => u.Email.ToLower().Equals(userView.Email.ToLower()));
            _mapper.Map(userView, existingUser);

            await _db.SaveChangesAsync();
            return userView;
        }

        public SignInResult SignInUser(string emailOrUserName, string password)
        {
            // create user session
            throw new NotImplementedException();
        }

        private void HashUserPassword(AppUser user) {
            string salt = null;
            var passwordHash = _pwdManager.GeneratePasswordHash(user.Password, out salt);

            user.Password = passwordHash;
            user.Salt = salt;
        }

        private async Task<AppUser> GetIfExists(Expression<Func<AppUser, bool>> exp) {

            var existedUser = await _db.Users.SingleOrDefaultAsync(exp);
            if (existedUser == null)
                throw new KeyNotFoundException("There is no user for such condition!");

            return existedUser;
        }

        #endregion
    }
}