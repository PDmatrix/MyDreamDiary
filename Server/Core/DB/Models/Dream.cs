using System;

namespace DB.Models
{
    public class Dream
    {
        public int Id { get; set; }
        public string Content { get; set; }
        public DateTime? DreamDate { get; set; }

        public Post Post { get; set; }
    }
}
