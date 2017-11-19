using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Linq;

namespace blog.Pages
{
    public class ArchiveModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<ArchiveModel> _logger;

        //one list of posts per year
        public ILookup<int, PostMeta> Posts;

        public List<int> YearsWithPosts => Posts.Select(p => p.Key).ToList();

        public ArchiveModel(ApplicationDbContext db, ILogger<ArchiveModel> logger)
        {
            _db = db;

            _logger = logger;
        }

        public async Task OnGetAsync()
        {
            var query = new GetPostsListMetaQuery(_db);

            var posts = await query.ExecuteAsync();

            Posts = posts.ToLookup(post => post.Published.Year);
        }
    }
}
