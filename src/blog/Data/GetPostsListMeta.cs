using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Data
{
    public class GetPostsListMetaQuery
    {
        private readonly ApplicationDbContext _db;

        public GetPostsListMetaQuery(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<PostMeta>> ExecuteAsync(bool publishedOnly = true)
        {
            IQueryable<Post> posts = _db.Posts;

            if(publishedOnly)
            {
                posts = posts.Where(p => p.Published.HasValue);
            }

            return await posts
                .OrderByDescending(post => post.Published)
                .Select(post => new PostMeta {
                    Id = post.Id,
                    Title = post.Title,
                    Published = post.Published.Value,
                    Slug = post.Slug
                }).ToListAsync();
        }
    }
}
