using DB.Entity;

namespace DB
{
    public class UserLike
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int PostId { get; set; }

        public Post Post { get; set; }
        public IdentityUser User { get; set; }
    }
}
