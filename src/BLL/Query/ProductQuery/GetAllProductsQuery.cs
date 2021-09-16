using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BLL.Contract;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Query.ProductQuery
{
    public class GetAllProductsQuery : IRequest<Result<List<Product>>>
    {
        public ProductRequest QueryRequest { get; set; }
        public GetAllProductsQuery(ProductRequest queryRequest)
        {
            QueryRequest = queryRequest;
        }

        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<List<Product>>>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IProductRepository _productRepository;

            public GetAllProductsQueryHandler(IBrandRepository brandRepository, IProductRepository productRepository)
            {
                _brandRepository = brandRepository;
                _productRepository = productRepository;
            }

            public async Task<Result<List<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.QueryAll(entity => entity.Code.Contains(request.QueryRequest.Code)
                ).Include(x => x.Brand).ToListAsync(cancellationToken);
                
                if (product.Count == 0)
                {
                    return Result.Failure<List<Product>>("No Data Found.");
                }

                return Result.Success(product);
            }
        }
    }
}