using System;

namespace DB.Dto
{
	public class CommentDtoOut
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime DateCreated { get; set; }
	}
}