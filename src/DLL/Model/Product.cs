using System.Collections.Generic;

namespace DLL.Model
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public List<BrandProduct> BrandProducts { get; set; }
    }
}