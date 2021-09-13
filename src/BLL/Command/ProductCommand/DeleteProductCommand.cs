using System.Threading;
using System.Threading.Tasks;
using CSharpFunctionalExtensions;
using DLL.Model;
using DLL.Repository;
using DLL.UoW;
using MediatR;

namespace BLL.Command.ProductCommand
{
    public class DeleteProductCommand : IRequest<Result<Product>>
    {
        public int Id { get; set; }

        public DeleteProductCommand(int id)
        {
            Id = id;
        }
        
        public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Result<Product>>
        {
            private readonly IUnitOfWork _unitOfWork;
            private readonly IProductRepository _productRepository;

            public DeleteProductCommandHandler(IUnitOfWork unitOfWork,IProductRepository productRepository)
            {
                _unitOfWork = unitOfWork;
                _productRepository = productRepository;
            }

            public async Task<Result<Product>> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
            {
                var product = await _productRepository.FirstOrDefaultAsync(x => x.Id == request.Id);

                if (product == null)
                {
                    return Result.Failure<Product>("No Data Found.");
                }
                
                await _productRepository.Delete(product);
                
                if (await _unitOfWork.Commit())
                {
                    return Result.Success(product);
                }
                
                return Result.Failure<Product>("Something went wrong. Please try again later.");
            }
        }
    }
}