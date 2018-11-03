using System.Collections.Generic;

namespace DB.Entity
{
    public class IdentityUser
    {
        public IdentityUser()
        {
            Comment = new HashSet<Comment>();
            Dream = new HashSet<Dream>();
            Post = new HashSet<Post>();
        }

        public string Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public ICollection<Comment> Comment { get; set; }
        public ICollection<Dream> Dream { get; set; }
        public ICollection<Post> Post { get; set; }
    }
}
