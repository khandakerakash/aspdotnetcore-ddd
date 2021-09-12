using System.Threading;
using System.Threading.Tasks;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using MediatR;

namespace BLL.Command.ProductCommand
{
    public class DeleteProductCommand : IRequest<Product>
    {
        public int Id { get; set; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
        
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Product>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProductRepository _productRepository;

            public DeleteProductCommandHandler(IUnitOfWork unitOfWork,IProductRepository productRepository)
            {
                _unitOfWork = unitOfWork;
                _productRepository = productRepository;
            }

            public async Task<Product> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (product != null)
                {
                    await _productRepository.Delete(product);
                    await _unitOfWork.Commit();
                }

                return null;
            }
        }
    }
}