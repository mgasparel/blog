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

        public int PageCount { get; set; }

        public int PageNum { get; set; }

        public IndexModel(ApplicationDbContext db, ILogger<IndexModel> logger)
        {
            _db = db;

            _logger = logger;
        }

        public async Task OnGetAsync([FromRoute] int pageNum = 1)
        {
            PageNum = pageNum;

            PageCount = await GetPageCountAsync();

            var query = new GetPostsQuery(_db);

            Posts = await query.ExecuteAsync(page: pageNum, pageSize: 5);
        }

        private async Task<int> GetPageCountAsync()
        {
            var postCountQuery = new GetPostCountQuery(_db);

            var postCount = await postCountQuery.ExecuteAsync(publishedOnly: true);

            return (postCount + 4) / 5;
        }
    }
}
