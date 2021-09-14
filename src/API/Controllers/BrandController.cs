using System.Threading.Tasks;
using BLL.Command.BrandCommand;
using BLL.Utils;
using DLL.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private IMediator _mediator;
        protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

        [HttpPost]
        public async Task<IActionResult> CreateBrand(Brand brand)
        {
            var response = await _mediator.Send(new CreateBrandCommand(brand.Name));
            if (response.IsSuccess) return Ok(Envelope.Ok(response.Value));
            return UnprocessableEntity(Envelope.Error(response.Error));
        }
    }
}