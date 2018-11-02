using System;
using System.Collections.Generic;

namespace DB.Dto
{
    public class PageDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public IEnumerable<string> Tags { get; set; }
        public string Username { get; set; }
        public int LikesCount { get; set; }
        public DateTime DateCreated { get; set; }
   }
}