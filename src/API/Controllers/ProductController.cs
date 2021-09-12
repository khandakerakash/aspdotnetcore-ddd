using System.Threading.Tasks;
using BLL.Command;
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

        [HttpPost]
        public async Task<IActionResult> CreateProduct(Product product)
        {
            return Ok(await Mediator.Send(new CreateProductCommand(product.Name, product.Description, product.Price)));
        }
    }
}