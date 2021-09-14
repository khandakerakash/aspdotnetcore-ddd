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
        //public List<BrandProduct> BrandProducts { get; set; }

        public CreateProductCommand(string code, string name, string description, decimal productPrice, decimal price)
        {
            Code = code;
            Name = name;
            Description = description;
            Price = price;
            //this.BrandProducts = new List<BrandProduct>();
        }
        
        public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, Result<Product>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProductRepository _productRepository;

            public CreateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
            {
                _unitOfWork = unitOfWork;
                _productRepository = productRepository;
            }

            public async Task<Result<Product>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
            {
                if (request.Code.HasNoValue())
                {
                    return Result.Failure<Product>("Product code must not have empty.");
                }
                
                var product = new Product()
                {
                    Code = request.Code,
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price,
                    //BrandProducts = request.BrandProducts.FirstOrDefault(x=>x.ProductId)
                };
                
                await _productRepository.CreateAsync(product);
                
                if (!await _unitOfWork.Commit())
                {
                    return Result.Failure<Product>("Something went wrong. Please try again later.");
                }
                
                return  Result.Success(product);
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