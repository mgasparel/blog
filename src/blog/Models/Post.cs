using System;
using System.Collections.Generic;
using blog.Data;

namespace blog.Models
{
    public class Post
    {
        private DateTime? _updated;

        public Guid Id { get; set; }

        public string Title { get; set; }

        public string Slug => Title.Slugify(100);

        public string Body { get; set; }

        public DateTime Created { get; set; }

        public DateTime? Published { get; set; }

        public DateTime Updated {
            get{
                if(_updated == null)
                {
                    return Created;
                }

                return _updated.Value;
            }
            set{
                _updated = value;
            }
        }

        public string AuthorId { get; set; }

        public ApplicationUser Author { get; set; }

        public ICollection<PostTag> PostTags { get; set; } = new List<PostTag>();

        public Post()
        {
            Created = DateTime.Now;
        }
    }
}
