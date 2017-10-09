using System.Collections.Generic;
using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace blog
{
    public class IndexModel: PageModel
    {
        private readonly ILogger _logger;

        private readonly ApplicationDbContext _db;

        public IEnumerable<Post> Posts;

        public bool HasPosts => Posts.Any();

        [TempData]
        public string Message { get; set; }

        public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;

            _logger = logger;
        }

        public void OnGet()
        {
            Posts = _db.Posts.ToList();
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            _db.Posts.Attach(new Post{ Id = id }).State = EntityState.Deleted;

            await _db.SaveChangesAsync();

            Message = "Post deleted successfully!";

            return RedirectToPage();
        }
    }
}
