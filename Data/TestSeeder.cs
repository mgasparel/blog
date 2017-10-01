using System.Linq;
using System.Threading.Tasks;
using blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace blog
{
    public static class TestSeeder
    {
        public static void EnsureDbSeeded(ApplicationDbContext db, UserManager<ApplicationUser> userManager, ILogger logger){
            //This method is used to seed the in-memory database during initial early development.
            //Fill this out depending on your needs

            //This will be removed in the future once we move to a persitent data store
        }
    }
}