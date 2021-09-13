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
            var response = await Mediator.Send(new GetAllProductsQuery());
            if(response.IsSuccess) return Ok(response.Value);
            return UnprocessableEntity(response.Error);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAProduct(int id)
        {
            var response = await Mediator.Send(new GetAProductQuery(id));
            if (response.IsSuccess) return Ok(response.Value);
            return UnprocessableEntity(response.Error);
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var response = await Mediator.Send(new CreateProductCommand(product.Name, product.Description, product.Price));
            if (response.IsSuccess) return Ok(response.Value);
            return UnprocessableEntity(response.Error);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Product product, int id)
        {
            var response =
                await Mediator.Send(new UpdateProductCommand(product.Name, product.Description, product.Price, id));
            if (response.IsSuccess) return Ok(response.Value);
            return UnprocessableEntity(response.Error);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await Mediator.Send(new DeleteProductCommand(id));
            if (response.IsSuccess) return Ok(response.Value);
            return UnprocessableEntity(response.Error);
        }
    }
}