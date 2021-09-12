using System.Threading;
using System.Threading.Tasks;
using DLL.Model;
using DLL.Repository;
using MediatR;

namespace BLL.Query.ProductQuery
{
    public class GetAProductQuery : IRequest<Product>
    {
        public int Id { get; set; }

        public GetAProductQuery(int id)
        {
            Id = id;
        }

        public class GetAProductQueryHandler : IRequestHandler<GetAProductQuery, Product>
        {
            private readonly IProductRepository _productRepository;

            public GetAProductQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Product> Handle(GetAProductQuery request, CancellationToken cancellationToken)
            {
                return await _productRepository.FirstOrDefaultAsync(x => x.Id == request.Id);
            }
        }
    }
}