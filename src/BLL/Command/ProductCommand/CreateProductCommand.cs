using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using MediatR;

namespace BLL.Command.ProductCommand
{
    public class CreateProductCommand : IRequest<Result<Product>>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public CreateProductCommand(string name, string description, decimal price)
        {
            Name = name;
            Description = description;
            Price = price;
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
                var product = new Product()
                {
                    Name = request.Name,
                    Description = request.Description,
                    Price = request.Price
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
}