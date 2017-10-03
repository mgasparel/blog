using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace blog
{
    public class PostModel: PageModel
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<PostModel> _logger;

        public Post Post { get; set; } 

        public PostModel(ApplicationDbContext db, ILogger<PostModel> logger)
        {
            _db = db;

            _logger = logger;
        }

        public IActionResult OnGet([FromRoute] string slug)
        {
            Post = _db.Posts.FirstOrDefault(x => x.Slug == slug);

            if(Post == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}