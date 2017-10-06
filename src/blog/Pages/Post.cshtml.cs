using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace blog
{
    public class PostModel: PageModel
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<PostModel> _logger;

        public Post Post { get; set; } 

        public string TagNames
        {
            get
            {
                if(Post.PostTags.Count > 0)
                {
                    return Post.PostTags
                        .Select(x => x.Tag.Name)
                        .Aggregate((list, tagName) => list + "," + tagName);
                }
                else
                {
                    return string.Empty;
                }
            }
        }

        public PostModel(ApplicationDbContext db, ILogger<PostModel> logger)
        {
            _db = db;

            _logger = logger;
        }

        public IActionResult OnGet([FromRoute] string slug)
        {
            Post = _db.Posts
                .Include(x => x.PostTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefault(x => x.Slug == slug);

            if(Post == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
