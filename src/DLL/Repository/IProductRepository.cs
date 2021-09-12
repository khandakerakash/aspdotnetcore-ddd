using DLL.Model;

namespace DLL.Repository
{
    public interface IProductRepository : IRepositoryBase<Product>
    {
        
    }

    public class ProductRepository : RepositoryBase<Product>, IProductRepository
    {
        public ProductRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}