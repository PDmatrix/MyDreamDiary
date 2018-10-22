namespace DB.Models
{
    public class PostTag
    {
        public int? PostId { get; set; }
        public int? TagId { get; set; }
        public int Id { get; set; }

        public Post Post { get; set; }
        public Tag Tag { get; set; }
    }
}
