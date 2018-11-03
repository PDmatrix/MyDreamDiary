using System;
using System.Collections.Generic;
using DB.Dto;

namespace DB.OutputDto
{
	public class GetPostDtoOut
	{
		public string Title { get; set; }
		public string Content { get; set; }
		public string Username { get; set; }
		public IEnumerable<CommentDtoOut> Comments { get; set; }
		public int LikesCount { get; set; }
		public DateTime DateCreated { get; set; }
		public IEnumerable<string> Tags { get; set; }
		public int Id { get; set; }
	}
}