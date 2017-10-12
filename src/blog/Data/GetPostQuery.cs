using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Data
{
    public class GetPostQuery
    {
        private readonly ApplicationDbContext _db;

        public GetPostQuery(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Post> ExecuteAsync(string slug)
        {
            return await _db.Posts
                .Include(x => x.Author)
                .Include(x => x.PostTags)
                .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Slug == slug);
        }
    }
}
