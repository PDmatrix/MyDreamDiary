using System;

namespace DB.OutputDto
{
	public class AddDreamDtoOut
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime DreamDate { get; set; }
	}
}