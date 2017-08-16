using Microsoft.AspNet.Identity.EntityFramework;
using TestWebApp.Models;

namespace TestWebApp.DAL
{
    public class TestAppContext: IdentityDbContext<AppUser>
    {
        public TestAppContext()
            : base("TestAppContext", throwIfV1Schema: false)
        {
        }

        public static TestAppContext Create() {
            return new TestAppContext();
        }
    }
}