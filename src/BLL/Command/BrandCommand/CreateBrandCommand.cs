#nullable enable
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BLL.Utils.Extensions;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using MediatR;

namespace BLL.Command.BrandCommand
{
    public class CreateBrandCommand : IRequest<Result<Brand>>
    {
        public string Name { get; set; }
        public List<Product>? Products { get; set; }

        public CreateBrandCommand(string name)
        {
            Name = name;
            this.Products = new List<Product>();
        }
        
        public class CreateBrandCommandHandler : IRequestHandler<CreateBrandCommand, Result<Brand>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IBrandRepository _brandRepository;

            public CreateBrandCommandHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository)
            {
                _unitOfWork = unitOfWork;
                _brandRepository = brandRepository;
            }

            public async Task<Result<Brand>> Handle(CreateBrandCommand request, CancellationToken cancellationToken)
            {
                if (request.Name.HasNoValue())
                {
                    return Result.Failure<Brand>("Brand name must not have empty.");
                }
                
                var brand = new Brand()
                {
                    Name = request.Name
                };

                await _brandRepository.CreateAsync(brand);

                if (!await _unitOfWork.Commit())
                {
                    return Result.Failure<Brand>("Something went wrong. Please try again later.");
                }

                return Result.Success(brand);
            }
        }
    }
}