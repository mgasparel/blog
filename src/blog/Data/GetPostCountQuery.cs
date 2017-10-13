using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blog.Models;
using Microsoft.EntityFrameworkCore;

namespace blog.Data
{
    public class GetPostCountQuery
    {
        private readonly ApplicationDbContext _db;

        public GetPostCountQuery(ApplicationDbContext db)
        {
            _db = db;
        }

        public Task<int> ExecuteAsync(bool publishedOnly = true)
        {
            if(publishedOnly)
            {
                return _db.Posts.CountAsync(p => p.Published.HasValue);
            }

            return _db.Posts.CountAsync();
        }
    }
}
