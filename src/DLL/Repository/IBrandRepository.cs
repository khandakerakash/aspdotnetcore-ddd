using DLL.Model;

namespace DLL.Repository
{
    public interface IBrandRepository : IRepositoryBase<Brand>
    {
        
    }

    public class BrandRepository : RepositoryBase<Brand>, IBrandRepository
    {
        public BrandRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}