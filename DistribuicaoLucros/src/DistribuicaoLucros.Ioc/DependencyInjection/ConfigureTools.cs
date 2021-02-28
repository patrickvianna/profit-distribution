using Microsoft.Extensions.DependencyInjection;
using DistribuicaoLucros.Domain.Tools;
using DistribuicaoLucros.Domain.Interfaces.Tools;

namespace DistribuicaoLucros.Ioc.DependencyInjection
{
    public class ConfigureTools
    {
        public static void ConfigureDependenciesTools(IServiceCollection services)
        {
            services.AddScoped<IDateTimeTools, DateTimeTools>();
        }
    }
}
