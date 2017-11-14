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

        public int PageNum { get; set; }

        public int PageSize { get; set; }

        public Paginator Paginator { get; set; }

        [TempData]
        public string Message { get; set; }

        public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;

            _logger = logger;

            PageSize = 10;
        }

        public async Task OnGetAsync([FromQuery] int pageNum = 1)
        {
            PageNum = pageNum;

            var query = new GetPostsQuery(_db);

            Posts = await query.ExecuteAsync(PageNum, PageSize, publishedOnly: false);

            Posts = Posts
                .OrderBy(p => p.Published.HasValue)
                .ThenByDescending(p => p.Published)
                .ThenByDescending(p => p.Updated);

            Paginator = new Paginator(pageNum, await GetPageCountAsync());
        }

        public async Task<IActionResult> OnPostDeleteAsync(Guid id)
        {
            _db.Posts.Attach(new Post{ Id = id }).State = EntityState.Deleted;

            await _db.SaveChangesAsync();

            Message = "Post deleted successfully!";

            return RedirectToPage();
        }

        private async Task<int> GetPageCountAsync()
        {
            var postCountQuery = new GetPostCountQuery(_db);

            int postCount = await postCountQuery.ExecuteAsync(publishedOnly: false);

            return (postCount + PageSize - 1) / PageSize;
        }
    }
}
