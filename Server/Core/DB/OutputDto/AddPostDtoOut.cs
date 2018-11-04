using System;

namespace DB.OutputDto
{
	public class AddPostDtoOut
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime DateCreated { get; set; }
	}
}