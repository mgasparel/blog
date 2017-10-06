using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace blog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<IndexModel> _logger;

        public IEnumerable<Post> Posts { get; set; }

        public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;

            _logger = logger;
        }

        public void OnGet()
        {
            Posts = _db.Posts
                .Include(x => x.PostTags)
                .ThenInclude(x => x.Tag)
                .Include(p => p.Author)
                .ToList();
        }
    }
}
