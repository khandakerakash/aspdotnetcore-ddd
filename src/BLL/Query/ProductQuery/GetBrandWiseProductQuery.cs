using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BLL.Utils.Extensions;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Query.ProductQuery
{
    public class GetBrandWiseProductQuery : IRequest<Result<List<Product>>>
    {
        public int BrandId { get; set; }

        public GetBrandWiseProductQuery(int brandId)
        {
            BrandId = brandId;
        }

        public class GetBrandWiseProductQueryHandler : IRequestHandler<GetBrandWiseProductQuery, Result<List<Product>>>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IProductRepository _productRepository;

            public GetBrandWiseProductQueryHandler(IBrandRepository brandRepository, IProductRepository productRepository)
            {
                _brandRepository = brandRepository;
                _productRepository = productRepository;
            }

            public async Task<Result<List<Product>>> Handle(GetBrandWiseProductQuery request, CancellationToken cancellationToken)
            {
                var brandIdValidation = await BrandIdValidate(request.BrandId);
                
                if (!brandIdValidation)
                {
                    return Result.Failure<List<Product>>("Invalid brand Id.");
                }

                var product = await _productRepository.QueryAll(x=>x.BrandId == request.BrandId).ToListAsync(cancellationToken);

                if (product.Count == 0)
                {
                    return Result.Failure<List<Product>>("No Data Found.");
                }
                
                return Result.Success(product);
            }
            
            private async Task<bool> BrandIdValidate(int requestBrandId)
            {
                var isBrandExists = await _brandRepository.FirstOrDefaultAsync(x => x.BrandId == requestBrandId);
                if (isBrandExists.HasValue())
                    return true;
                return false;
            }
        }
    }
}