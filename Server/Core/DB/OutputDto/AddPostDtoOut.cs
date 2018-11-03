using System;

namespace DB.Dto
{
	public class AddPostDtoOut
	{
		public int Id { get; set; }
		public string Title { get; set; }
		public DateTime DateCreated { get; set; }
	}
}