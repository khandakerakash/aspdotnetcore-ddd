using System.Reflection;
using BLL.Command.ProductCommand;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace BLL
{
    public static class BLLDependency
    {
        public static void BllDependency(this IServiceCollection services)
        {
            services.AddMediatR(Assembly.GetExecutingAssembly());
            RegisterFluentRequestValidator(services);
        }

        private static void RegisterFluentRequestValidator(IServiceCollection service)
        {
            service.AddTransient<IValidator<CreateProductCommand>, ProductCreateRequestValidator>();
        }
    }
}