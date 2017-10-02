using System.Collections.Generic;
using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace blog
{
    public class IndexModel: PageModel
    {
        private readonly ILogger _logger;

        private readonly ApplicationDbContext _db;

        public IEnumerable<Post> Posts;

        public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;

            _logger = logger;
        }

        public void OnGet()
        {
            Posts = _db.Posts.ToList();
        }
    }
}