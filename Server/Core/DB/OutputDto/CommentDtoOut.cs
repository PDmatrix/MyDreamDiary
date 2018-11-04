using System;

namespace DB.OutputDto
{
	public class CommentDtoOut
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime DateCreated { get; set; }
	}
}