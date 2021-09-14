using System.Threading;
using System.Threading.Tasks;
using BLL.Utils.Extensions;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using MediatR;

namespace BLL.Query.ProductQuery
{
    public class GetAProductQuery : IRequest<Result<Product>>
    {
        public int ProductId { get; set; }

        public GetAProductQuery(int id)
        {
            ProductId = id;
        }

        public class GetAProductQueryHandler : IRequestHandler<GetAProductQuery, Result<Product>>
        {
            private readonly IProductRepository _productRepository;

            public GetAProductQueryHandler(IProductRepository productRepository)
            {
                _productRepository = productRepository;
            }

            public async Task<Result<Product>> Handle(GetAProductQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.FirstOrDefaultAsync(x => x.ProductId == request.ProductId);

                if (product.HasNoValue())
                {
                    return Result.Failure<Product>("No Data Found.");
                }
                
                return Result.Success(product);
            }
        }
    }
}