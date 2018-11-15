using System;
using System.Collections.Generic;

namespace DB.OutputDto
{
	public class GetUserDtoOut
	{
		public string Name { get; set; }
		public string Email { get; set; }
		public IEnumerable<CommentDtoOut> Comments { get; set; }
		public IEnumerable<UserPostDtoOut> Posts { get; set; }
		public IEnumerable<UserDreamDtoOut> Dreams { get; set; }
		public string Id { get; set; }
	}

	public class UserPostDtoOut
	{
		public int Id { get; set; }
		public string Title { get; set; }
	}
	
	public class UserDreamDtoOut
	{
		public string Content { get; set; }
		public DateTime DreamDate { get; set; }
		public int Id { get; set; }
	}
}