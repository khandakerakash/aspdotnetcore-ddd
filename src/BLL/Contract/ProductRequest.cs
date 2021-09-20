using System;

namespace BLL.Contract
{
    public class ProductRequest: GeneralPaginationQuery
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public int BrandId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}