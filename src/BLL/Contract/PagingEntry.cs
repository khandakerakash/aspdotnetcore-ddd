using System.Collections.Generic;
using BLL.Utils;

namespace BLL.Contract
{
    public class PagingEntry<T> : IPagedList<T>
    {
        public int IndexFrom { get; set; }
        public int PageNumber { get; set;}
        public int PageSize { get; set;}
        public int TotalCount { get;set; }
        public int TotalPages { get;set; }
        public IList<T> Items { get;  set;}= new List<T>();
        public bool HasPreviousPage { get; set;}
        public bool HasNextPage { get; set; }

        public PagingEntry(IPagedList<T> item)
        {
            Items = item.Items;
            IndexFrom = item.IndexFrom;
            PageNumber = item.PageNumber;
            PageSize = item.PageSize;
            TotalCount = item.TotalCount;
            TotalPages = item.TotalPages;
            HasPreviousPage = item.HasPreviousPage;
            HasNextPage = item.HasNextPage;
        }

        public PagingEntry()
        {
            
        }
    }
}