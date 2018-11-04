using System.Collections.Generic;

namespace DB.OutputDto
{
    public class Page<T>
    {
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
        public IEnumerable<T> Records { get; set; }
        public int TotalPages { get; set; }
    }
}