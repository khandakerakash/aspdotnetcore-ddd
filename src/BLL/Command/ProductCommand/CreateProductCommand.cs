using System.Threading;
using System.Threading.Tasks;
using BLL.Utils.Extensions;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using FluentValidation;
using MediatR;

namespace BLL.Command.ProductCommand
{
    public class CreateProductCommand : IRequest<Result<Product>>
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int BrandId { get; set; }
        
        public CreateProductCommand(string code, string name, string description, decimal price, int productBrandId)
        {
            Code = code;
            Name = name;
            Description = description;
            Price = price;
            BrandId = productBrandId;
        }
        
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IBrandRepository _brandRepository;
            private readonly IProductRepository _productRepository;

            public CreateProductCommandHandler(IUnitOfWork unitOfWork, IBrandRepository brandRepository, IProductRepository productRepository)
            {
                _unitOfWork = unitOfWork;
                _brandRepository = brandRepository;
                _productRepository = productRepository;
            }

            public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                if (request.Code.HasNoValue())
                {
                    return Result.Failure<Product>("Product code must not have empty.");
                }

                var brandIdValidation = await BrandIdValidate(request.BrandId);
                
                if (!brandIdValidation)
                {
                    return Result.Failure<Product>("Invalid brand Id.");
                }
                
                var product = new Product()
                {
                    Code = request.Code,
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    BrandId = request.BrandId
                };
                
                await _productRepository.CreateAsync(product);
                
                if (!await _unitOfWork.Commit())
                {
                    return Result.Failure<Product>("Something went wrong. Please try again later.");
                }
                
                return  Result.Success(product);
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
    
    public class ProductCreateRequestValidator : AbstractValidator<CreateProductCommand>
    {
        public ProductCreateRequestValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty().MinimumLength(4).MaximumLength(120);
            RuleFor(x => x.Description).NotNull().NotEmpty().MinimumLength(10).MaximumLength(255);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0).WithMessage("The price must not have empty.");
        }
    }
}