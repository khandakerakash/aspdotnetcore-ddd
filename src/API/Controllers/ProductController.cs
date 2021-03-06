using System.Threading.Tasks;
using API.Contracts.Request;
using BLL.Command.ProductCommand;
using BLL.Contract;
using BLL.Query.ProductQuery;
using BLL.Utils;
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
        public async Task<IActionResult> GetAllProducts([FromQuery] ProductRequest queryRequest)
        {
            var response = await Mediator.Send(new GetAllProductsQuery(queryRequest));
            if(response.IsSuccess) return Ok(Envelope.Ok(response.Value));
            return UnprocessableEntity(Envelope.Error(response.Error));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetAProduct(int id)
        {
            var response = await Mediator.Send(new GetAProductQuery(id));
            if(response.IsSuccess) return Ok(Envelope.Ok(response.Value));
            return UnprocessableEntity(Envelope.Error(response.Error));
        }
        
        [HttpGet("brand-wise-product/{brandId}")]
        public async Task<IActionResult> GetBrandWieProduct(int brandId)
        {
            var res = await Mediator.Send(new GetBrandWiseProductQuery(brandId));
            if (res.IsSuccess) return Ok(Envelope.Ok(res.Value));
            return Ok(Envelope.Error(res.Error));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            var response = await Mediator.Send(new CreateProductCommand(product.Code, product.Name, product.Description, product.Price, product.BrandId));
            if(response.IsSuccess) return Ok(Envelope.Ok(response.Value));
            return UnprocessableEntity(Envelope.Error(response.Error));
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(Product product, int id)
        {
            var response =
                await Mediator.Send(new UpdateProductCommand(product.Name, product.Description, product.Price, id));
            if(response.IsSuccess) return Ok(Envelope.Ok(response.Value));
            return UnprocessableEntity(Envelope.Error(response.Error));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteProduct(int id)
        {
            var response = await Mediator.Send(new DeleteProductCommand(id));
            if(response.IsSuccess) return Ok(Envelope.Ok(response.Value));
            return UnprocessableEntity(Envelope.Error(response.Error));
        }
    }
}