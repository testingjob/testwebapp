using System.Data.Entity;
using TestWebApp.Models;

namespace TestWebApp.DAL
{
    public class TestAppContext: DbContext
    {
        public TestAppContext() : base("TestAppContext") {}

        public DbSet<AppUser> Users { get; set; }
    }
}