using System.Threading.Tasks;
using API.Contracts.Request;
using BLL.Command.BrandCommand;
using BLL.Query.BrandQuery;
using BLL.Utils;
using CSharpFunctionalExtensions;
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

        [HttpGet]
        public async Task<IActionResult> GetAllBrand()
        {
            var res = await Mediator.Send(new GetAllBrandsQuery());
            if (res.IsSuccess) return Ok(Envelope.Ok(res.Value));
            return UnprocessableEntity(Envelope.Error(res.Error));
        }
        
        [HttpPost]
        public async Task<IActionResult> CreateBrand(CreateBrandDto brand)
        {
            var res = await Mediator.Send(new CreateBrandCommand(brand.Name));
            if (res.IsSuccess) return Ok(Envelope.Ok(res.Value));
            return UnprocessableEntity(Envelope.Error(res.Error));
        }
    }
}