using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Query.ProductQuery
{
    public class GetAllProductsQuery : IRequest<Result<List<Product>>>
    {
        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<Product>>>
        {
            private readonly IProductRepository _productRepository;

            public GetAllProductsQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Result<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var product =  await _productRepository.QueryAll(null).ToListAsync(cancellationToken);

                if (product.Count == 0)
                {
                    return Result.Failure<List<Product>>("No Data Found.");
                }

                return Result.Success(product);
            }
        }
    }
}