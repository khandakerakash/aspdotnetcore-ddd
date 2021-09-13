using System;
using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using MediatR;

namespace BLL.Command.ProductCommand
{
    public class UpdateProductCommand : IRequest<Result<Product>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }

        public UpdateProductCommand(string name, string description, decimal price, int id)
        {
            Id = id;
            Name = name;
            Description = description;
            Price = price;
        }
        
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Result<Product>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProductRepository _productRepository;

            public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
            {
                _unitOfWork = unitOfWork;
                _productRepository = productRepository;
            }

            public async Task<Result<Product>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (product == null)
                {
                    return Result.Failure<Product>("No Data Found.");
                }
                
                product.Name = request.Name;
                product.Description = request.Description;
                product.Price = request.Price;
                await _productRepository.Update(product);
                
                if (await _unitOfWork.Commit())
                {
                    return Result.Success(product);
                }
                
                return Result.Failure<Product>("Something went wrong. Please try again later.");
            }
        }
    }
}