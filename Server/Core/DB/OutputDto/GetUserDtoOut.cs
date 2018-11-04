using System.Collections.Generic;

namespace DB.OutputDto
{
	public class GetUserDtoOut
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public IEnumerable<CommentDtoOut> Comments { get; set; }
		public IEnumerable<UserPostDtoOut> Posts { get; set; }
		public string Id { get; set; }
	}

	public class UserPostDtoOut
	{
		public int Id { get; set; }
		public string Title { get; set; }
	}
}