using System.Collections.Generic;
using BLL.Utils;

namespace API.Contracts.Models
{
    public class PagingEntry<T> : IPagedList<T>
    {
        public int IndexFrom { get; set; }
        public int PageNumber { get; set;}
        public int PageSize { get; set;}
        public int TotalCount { get;set; }
        public int TotalPages { get;set; }
        public IList<T> Items { get; set;}= new List<T>();
        public bool HasPreviousPage { get; set;}
        public bool HasNextPage { get; set; }
    }
}