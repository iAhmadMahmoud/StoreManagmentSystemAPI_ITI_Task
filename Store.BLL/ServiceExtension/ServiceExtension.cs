using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace Store.BLL
{
    public static class ServiceExtension
    {
        public static void AddBLLExtension(this IServiceCollection services)
        {
        
            services.AddScoped<IProductManager,ProductManager>();
            services.AddScoped<ICategoryManager,CategoryManager>();
            services.AddValidatorsFromAssembly(typeof(ServiceExtension).Assembly);
            services.AddAutoMapper(typeof(ServiceExtension).Assembly);
            services.AddScoped<IErrorMapper,ErrorMapper>();


        }
        
    }
}
