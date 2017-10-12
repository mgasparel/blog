using System;
using System.Collections.Generic;

namespace blog.Models
{
    public class Tag
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public ICollection<PostTag> Posts { get; } = new List<PostTag>();
    }
}
