using System;
using System.Collections.Generic;

namespace DB.Entity
{
    public class Post
    {
        public Post()
        {
            Comment = new HashSet<Comment>();
            PostTag = new HashSet<PostTag>();
            UserLike = new HashSet<UserLike>();
        }

        public int Id { get; set; }
        public int LikesCount { get; set; }
        public string Title { get; set; }
        public int DreamId { get; set; }
        public DateTime DateCreated { get; set; }
        public string UserId { get; set; }

        public Dream Dream { get; set; }
        public IdentityUser User { get; set; }
        public ICollection<Comment> Comment { get; set; }
        public ICollection<PostTag> PostTag { get; set; }
        public ICollection<UserLike> UserLike { get; set; }
    }
}
