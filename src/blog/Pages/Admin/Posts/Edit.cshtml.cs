using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace blog
{
    public class EditModel: PageModel
    {
        private readonly ApplicationDbContext _db;

        private readonly UserManager<ApplicationUser> _userManager;

        private readonly ILogger<EditModel> _logger;

        [BindProperty]
        public string Title { get; set; }

        [BindProperty]
        public string Body { get; set; }

        [BindProperty]
        public string Tags { get; set; }

        public bool IsPublished { get; set; }

        public EditModel(ApplicationDbContext db, UserManager<ApplicationUser> userManager, ILogger<EditModel> logger)
        {
            _db = db;

            _userManager = userManager;

            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync([FromRoute] string slug)
        {
            var post = await _db.Posts
                .Include(x => x.PostTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Slug == slug);

            if(post == null)
            {
                return NotFound();
            }

            Title = post.Title;

            Body = post.Body;

            Tags = GetTagNames(post);

            IsPublished = post.Published != null;

            return Page();
        }

        public async Task<IActionResult> OnPostPublishAsync([FromRoute] string slug)
        {
            var post = await _db.Posts
                .Include(x => x.PostTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Slug == slug);

            if(post == null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                await SavePostAsync(post, Tags, publish: true);

                return RedirectToPage("Index");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSaveAsync([FromRoute] string slug)
        {
            var post = await _db.Posts
                .Include(x => x.PostTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Slug == slug);

            if(post == null)
            {
                return NotFound();
            }

            if(ModelState.IsValid)
            {
                await SavePostAsync(post, Tags, publish: false);

                return RedirectToPage("Index");
            }

            return Page();
        }

        private async Task SavePostAsync(Post post, string tags, bool publish = false)
        {
            IEnumerable<Tag> tagList = GetTagList(tags);

            post.Title = Title;

            post.Body = Body;

            if(publish)
            {
                post.Published = DateTime.Now;
            }

            _db.RemoveRange(post.PostTags);

            await _db.SaveChangesAsync();

            _db.AddRange(
                tagList.Select(tag => new PostTag { Tag = tag, Post = post }));

            await _db.SaveChangesAsync();
        }

        private string GetTagNames(Post post)
        {
            if(post.PostTags.Count > 0)
            {
                return post.PostTags
                    .Select(x => x.Tag.Name)
                    .Aggregate((list, tagName) => list + "," + tagName);
            }
            else
            {
                return string.Empty;
            }
        }

        private IEnumerable<Tag> GetTagList(string tagNames)
        {
            IEnumerable<string> sanitizedNames = tagNames
                .Split(',')
                .Select(x => x.Trim());

            foreach(string name in sanitizedNames)
            {
                var tag = _db.Tags.FirstOrDefault(x => x.Name == name);

                if(tag == null)
                {
                    tag = new Tag{ Name = name };
                }

                yield return tag;
            }
        }
    }
}
