using System;

namespace DB.OutputDto
{
	public class GetDreamDtoOut
	{
		public int Id { get; set; }
		public string Content { get; set; }
		public DateTime DreamDate { get; set; }
	}
}