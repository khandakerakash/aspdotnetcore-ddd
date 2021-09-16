using System;

namespace BLL.Contract
{
    public class ProductRequest
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string BrandId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}