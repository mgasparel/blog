using System;
using System.Linq;
using System.Threading.Tasks;
using blog.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace blog
{
    public static class TestSeeder
    {
        public static async Task EnsureDbSeeded(
            ApplicationDbContext db,
            UserManager<ApplicationUser> userManager,
            ILogger logger,
            IConfiguration configuration)
        {
            if(!db.Users.Any())
            {
                string username = configuration["DefaultUser:Username"];
                string password = configuration["DefaultUser:Password"];
                string email = configuration["DefaultUser:Email"];

                if(email == null)
                {
                    logger.LogWarning("DeafultUser:Email was not found in any defined configuration.");
                }

                if(password == null)
                {
                    logger.LogWarning("DeafultUser:Password was not found in any defined configuration.");
                }

                if(email == null || password == null)
                {
                    logger.LogWarning("No default user created.");

                    return;
                }

                if(username == null)
                {
                    logger.LogInformation("DefaultUser:Username was not found in any defined configuration. Using DefaultUser:Email as the username.");

                    username = email;
                }

                await userManager.CreateAsync(new ApplicationUser{ UserName = username, Email = email}, password);
            }
        }
    }
}
