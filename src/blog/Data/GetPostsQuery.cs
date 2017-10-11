using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Data
{
    public class GetPostsQuery
    {
        private readonly ApplicationDbContext _db;

        public GetPostsQuery(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Post>> ExecuteAsync(int page = 1, int pageSize = 10, bool publishedOnly = true)
        {
            if(page < 1)
            {
                page = 1;
            }

            IQueryable<Post> posts = _db.Posts
                .Include(x => x.Author)
                .Include(x => x.PostTags)
                .ThenInclude(x => x.Tag);

            if(publishedOnly)
            {
                posts = posts.Where(p => p.Published.HasValue);
            }

            return await posts
                .OrderByDescending(x => x.Published)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }
    }
}