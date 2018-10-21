using System;

namespace DB.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public int PostId { get; set; }
        public DateTime? DateCreated { get; set; }
        public int UserId { get; set; }

        public Post Post { get; set; }
        public IdentityUser User { get; set; }
    }
}
