using System;
using System.Collections.Generic;
using System.Linq;
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

        [BindProperty]
        public string Tags { get; set; }

        public NewModel(ApplicationDbContext dbContext, UserManager<ApplicationUser> userManager, ILogger<NewModel> logger)
        {
            _dbContext = dbContext;

            _userManager = userManager;

            _logger = logger;
        }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostSaveAsync()
        {
            if(ModelState.IsValid)
            {
                await SavePostAsync(Title, Body, Tags);

                return Redirect("./Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostPublishAsync()
        {
            if(ModelState.IsValid)
            {
                await SavePostAsync(Title, Body, Tags, publish: true);

                return Redirect("./Index");
            }

            return Page();
        }

        private async Task SavePostAsync(string title, string body, string tags, bool publish = false)
        {
            IEnumerable<Tag> tagList = GetTagList(tags);

            var post = new Post()
            {
                Title = title,
                Body = body,
                AuthorId = _userManager.GetUserId(User)
            };

            if(publish)
            {
                post.Published = DateTime.Now;
            }

            _dbContext.AddRange(
                tagList.Select(tag => new PostTag { Tag = tag, Post = post }));

            await _dbContext.SaveChangesAsync();
        }

        private IEnumerable<Tag> GetTagList(string tagNames)
        {
            IEnumerable<string> sanitizedNames = tagNames
                .Split(',')
                .Select(x => x.Trim());

            foreach(string name in sanitizedNames)
            {
                var tag = _dbContext.Tags.FirstOrDefault(x => x.Name == name);

                if(tag == null)
                {
                    tag = new Tag{ Name = name };
                }

                yield return tag;
            }
        }
    }
}