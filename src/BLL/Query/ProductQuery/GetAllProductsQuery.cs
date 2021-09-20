using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using BLL.Contract;
using BLL.Utils;
using BLL.Utils.Extensions;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Query.ProductQuery
{
    public class GetAllProductsQuery : IRequest<Result<PagingEntry<Product>>>
    {
        public ProductRequest QueryRequest { get; set; }
        public GetAllProductsQuery(ProductRequest queryRequest)
        {
            QueryRequest = queryRequest;
        }

        public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, Result<PagingEntry<Product>>>
        {
            private readonly IBrandRepository _brandRepository;
            private readonly IProductRepository _productRepository;

            public GetAllProductsQueryHandler(IBrandRepository brandRepository, IProductRepository productRepository)
            {
                _brandRepository = brandRepository;
                _productRepository = productRepository;
            }

            public async Task<Result<PagingEntry<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
            {
                var product = new PagingEntry<Product>();
                
                if (request.QueryRequest.StartDate.HasValue() && request.QueryRequest.EndDate.HasValue())
                {
                     var productList = await _productRepository.QueryAll(null).Include(x=>x.Brand).ToPagedListAsync(request.QueryRequest.PageNumber,
                        request.QueryRequest.Pagination ? request.QueryRequest.PageSize : 100);
                     product = new PagingEntry<Product>(productList);
                }

                if (request.QueryRequest.Code.HasValue())
                {
                    var productList = await _productRepository.QueryAll(c=>c.Code == request.QueryRequest.Code).Include(x => x.Brand).ToPagedListAsync(request.QueryRequest.PageNumber,
                        request.QueryRequest.Pagination ? request.QueryRequest.PageSize : 100);
                    product = new PagingEntry<Product>(productList);
                }
                
                
                if (request.QueryRequest.Name.HasValue())
                {
                    var productList = await _productRepository.QueryAll(n=>n.Name == request.QueryRequest.Name).Include(x => x.Brand).ToPagedListAsync(request.QueryRequest.PageNumber,
                            request.QueryRequest.Pagination ? request.QueryRequest.PageSize : 100);
                    product = new PagingEntry<Product>(productList);
                }
                
                if (request.QueryRequest.BrandId !=0)
                {
                    var productList = await _productRepository.QueryAll(b=>b.BrandId == request.QueryRequest.BrandId).Include(x => x.Brand).ToPagedListAsync(request.QueryRequest.PageNumber,
                        request.QueryRequest.Pagination ? request.QueryRequest.PageSize : 100);
                    product = new PagingEntry<Product>(productList);;
                }
                
                if (product.TotalCount == 0)
                {
                    return Result.Failure<PagingEntry<Product>>("No Data Found.");
                }
                
                return Result.Success(product);
            }
        }
    }
}