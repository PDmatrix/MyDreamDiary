using System;

namespace DB.Entity
{
    public class Dream
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime DreamDate { get; set; }
        public string UserId { get; set; }

        public IdentityUser User { get; set; }
        public Post Post { get; set; }
    }
}
