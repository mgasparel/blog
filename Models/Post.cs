using System;
using blog.Data;

namespace blog.Models
{
    public class Post
    {
        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Body { get; set; }

        public DateTime Created { get; set; }

        public DateTime Published { get; set; }

        public DateTime Updated { get; set; }

        public ApplicationUser Author { get; set; }

        public Post()
        {
            Created = DateTime.Now;
        }
    }
}