using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BLL.Query.BrandQuery
{
    public class GetAllBrandsQuery : IRequest<Result<List<Brand>>>
    {
        public class GetAllBrandsQueryHandler : IRequestHandler<GetAllBrandsQuery, Result<List<Brand>>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IBrandRepository _brandRepository;

            public GetAllBrandsQueryHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
            {
                _unitOfWork = unitOfWork;
                _brandRepository = brandRepository;
            }

            public async Task<Result<List<Brand>>> Handle(GetAllBrandsQuery request, CancellationToken cancellationToken)
            {
                var brand = await _brandRepository.QueryAll(null).Include(x => x.Products).ToListAsync(cancellationToken);
                if (brand.Count == 0)
                {
                    return Result.Failure<List<Brand>>("No Data Found.");
                }
                
                return Result.Success(brand);
            }
        }
    }
}