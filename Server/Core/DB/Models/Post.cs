using System;
using System.Collections.Generic;

namespace DB.Models
{
    public class Post
    {
        public Post()
        {
            Comments = new HashSet<Comment>();
            Tags = new HashSet<Tag>();
        }

        public int Id { get; set; }
        public int? LikesCount { get; set; }
        public string Title { get; set; }
        public int DreamId { get; set; }
        public DateTime? DateCreated { get; set; }
        public int UserId { get; set; }

        public Dream Dream { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
    }
}
