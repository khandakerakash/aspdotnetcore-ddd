using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DLL.Model;
using DLL.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Query.ProductQuery
{
    public class GetAllProductsQuery : IRequest<List<Product>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, List<Product>>
        {
            private readonly IProductRepository _productRepository;

            public GetAllProductsQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<List<Product>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                return await _productRepository.QueryAll(null).ToListAsync(cancellationToken);
            }
        }
    }
}