using System.Collections.Generic;

namespace WebMVC.Models
{
    public class PagedData<T> where T : class
    {
        public IEnumerable<T> Data { get; set; }

        public int TotalPages { get; set; }

        public int CurrentPage { get; set; }
    }
}