using System;

namespace blog.Models
{
    public class PostMeta
    {
        public Guid Id { get; set; }

        public string Slug { get; set; }

        public DateTime Published { get; set; }

        public string Title { get; set; }
    }
}
