using AutoMapper;
using TestWebApp.Models;
using TestWebApp.ViewModels;

namespace TestWebApp.Mappings
{
    public class AutoMapperConfiguration
    {
        public MapperConfiguration Configure() {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile<TestWebAppProfile>();
            });
            return config;
        }
    }

    public class TestWebAppProfile: Profile
    {
        public TestWebAppProfile() {
            CreateMap<UserManageModel, AppUser>();
            CreateMap<AppUser, UserManageModel>();

            CreateMap<LoginViewModel, AppUser>();
            CreateMap<RegisterViewModel, AppUser>();
        }
    }
}