using System;
using System.Threading;
using System.Threading.Tasks;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using MediatR;

namespace BLL.Command.ProductCommand
{
    public class UpdateProductCommand : IRequest<Product>
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
        
        public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Product>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProductRepository _productRepository;

            public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IProductRepository productRepository)
            {
                _unitOfWork = unitOfWork;
                _productRepository = productRepository;
            }

            public async Task<Product> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (product != null)
                {
                    product.Name = request.Name;
                    product.Description = request.Description;
                    product.Price = request.Price;
                    
                    await _productRepository.Update(product);
                    await _unitOfWork.Commit();
                    return product;
                }

                return null;
            }
        }
    }
}