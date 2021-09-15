using System.Collections.Generic;

namespace DLL.Model
{
    public class Brand
    {
        public int BrandId { get; set; }
        public string Name { get; set; }
        public List<Product> Products { get; set; }
    }
}