using Microsoft.Extensions.DependencyInjection;
using DistribuicaoLucros.Domain.Interfaces.Service;
using DistribuicaoLucros.Service.Implementation;

namespace DistribuicaoLucros.Ioc.DependencyInjection
{
    public class ConfigureService
    {
        public static void ConfigureDependenciesService(IServiceCollection services)
        {
            services.AddScoped<IEmployeeService, EmployeeService>();
            services.AddScoped<IProfitDistributionService, ProfitDistributionService>();
        }
    }
}
