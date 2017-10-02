using System;
using System.Threading.Tasks;
using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace blog
{
    public class NewModel : PageModel
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<NewModel> _logger;

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Body { get; set; }

        public NewModel(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ILogger<NewModel> logger)
        {
            _dbContext = dbContext;

            _userManager = userManager;

            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostPublishAsync()
        {
            if(ModelState.IsValid)
            {
                await SavePostAsync(Title, Body);

                return Redirect("./Index");
            }

            return Page();
        }

        private async Task SavePostAsync(string title, string body)
        {
            var post = new Post()
            {
                Title = title,
                Body = body,
                Author = new ApplicationUser{ Id = _userManager.GetUserId(User) }
            };

            _dbContext.Posts.Add(post);

            await _dbContext.SaveChangesAsync();
        }
    }
}