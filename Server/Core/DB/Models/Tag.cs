using System.Collections.Generic;

namespace DB.Models
{
    public class Tag
    {
        public Tag()
        {
            PostTag = new HashSet<PostTag>();
        }
        
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<PostTag> PostTag { get; set; }
    }
}
