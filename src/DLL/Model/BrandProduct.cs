namespace DLL.Model
{
    public class BrandProduct
    {
        public int BrandId { get; set; }
        public Brand Brand { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }
    }
}