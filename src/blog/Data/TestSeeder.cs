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
            var user = new ApplicationUser();
            user.UserName = "me@test.com";
            user.Email = "me@test.com";

            userManager.CreateAsync(user, "asdASD123!@#");
        }
    }
}