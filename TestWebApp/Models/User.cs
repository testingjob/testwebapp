using Microsoft.AspNet.Identity.EntityFramework;

namespace TestWebApp.Models
{
    public class AppUser: IdentityUser
    {
        public string Age { get; set; }
    }
}