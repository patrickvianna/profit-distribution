using DistribuicaoLucros.Api.Middleware;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace DistribuicaoLucros.Api.Extensions
{
    public class GlobalExceptionHandlerExtensions
    {
        public static IServiceCollection AddGlobalExceptionHandler(IServiceCollection services)
        {
            return services.AddTransient<GlobalExceptionHandlerMiddleware>();
        }

        public static void UseGlobalExceptionHandler(IApplicationBuilder app)
        {
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();
        }
    }
}
