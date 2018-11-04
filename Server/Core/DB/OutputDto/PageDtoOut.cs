using System;
using System.Collections.Generic;

namespace DB.OutputDto
{
    public class PageDtoOut
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Username { get; set; }
        public int LikesCount { get; set; }
        public DateTime DateCreated { get; set; }
	    public int Id { get; set; }
	    public bool IsLiked { get; set; }
   }
}