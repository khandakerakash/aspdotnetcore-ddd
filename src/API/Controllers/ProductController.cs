using System.Threading.Tasks;
using BLL.Command;
using BLL.Command.ProductCommand;
using BLL.Query.ProductQuery;
using DLL.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpGet]
        public async Task<IActionResult> GetAllProducts()
        {
            return Ok(await Mediator.Send(new GetAllProductsQuery()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAProduct(int id)
        {
            return Ok(await Mediator.Send(new GetAProductQuery(id)));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var response = await Mediator.Send(new CreateProductCommand(product.Name, product.Description, product.Price));

            if (!response.IsSuccess)
            {
                return UnprocessableEntity(response.Error);
            }
            return Ok(response.Value);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Product product, int id)
        {
            var response =
                await Mediator.Send(new UpdateProductCommand(product.Name, product.Description, product.Price, id));

            if (!response.IsSuccess)
            {
                return UnprocessableEntity(response.Error);
            }
            
            return Ok(response.Value);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await Mediator.Send(new DeleteProductCommand(id));
            
            if (!response.IsSuccess)
            {
                return UnprocessableEntity(response.Error);
            }
            
            return Ok(response.Value);
        }
    }
}