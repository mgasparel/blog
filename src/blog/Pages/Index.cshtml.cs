using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Data;
using blog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace blog.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        private readonly ILogger<IndexModel> _logger;

        private readonly IConfiguration _configuration;

        public IEnumerable<Post> Posts { get; set; }

        public int PageSize { get; set; }

        public int PageCount { get; set; }

        public int PageNum { get; set; }

        public IndexModel(ApplicationDbContext db, IConfiguration configuration, ILogger<IndexModel> logger)
        {
            _db = db;

            _configuration = configuration;

            _logger = logger;

            if(!int.TryParse(configuration["BlogSettings:PostsPerPage"], out int pageSize))
            {
                PageSize = 5;
            }
            else
            {
                PageSize = pageSize;
            }
        }

        public async Task OnGetAsync([FromRoute] int pageNum = 1)
        {
            PageNum = pageNum;

            PageCount = await GetPageCountAsync();

            var query = new GetPostsQuery(_db);

            Posts = await query.ExecuteAsync(pageNum, PageSize);
        }

        private async Task<int> GetPageCountAsync()
        {
            var postCountQuery = new GetPostCountQuery(_db);

            int postCount = await postCountQuery.ExecuteAsync(publishedOnly: true);

            return (postCount + PageSize - 1) / PageSize;
        }
    }
}
