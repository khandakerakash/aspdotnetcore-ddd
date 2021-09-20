using System;

namespace API.Contracts.Request
{
    public class GeneralPaginationQuery
    {
        private const int MaxPageSize = 100;
        public int PageNumber { get; set; } = 1;
        public string OrderBy { get; set; }
        public bool Descending { get; set; } = true;
        public bool Pagination { get; set; } = true;
        private int _pageSize = 20;

        public int Skip() => (PageNumber - 1) * PageSize;

        public int PageSize
        {
            get => _pageSize; 
            set => _pageSize = (value > MaxPageSize || value <= 0) ? MaxPageSize : value;
        }

        public override string ToString()
        {
            return PageNumber + "_" + PageSize + "_" + DateTime.UtcNow.Day + "_" + DateTime.UtcNow.Hour + "_" +
                   OrderBy + "_" + Descending;
        }
    }
}