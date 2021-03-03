using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Firebase.Database;
using System.Threading.Tasks;
using DistribuicaoLucros.Infraestructure.Data.Repository;
using DistribuicaoLucros.Domain.Interfaces.Repository;

namespace DistribuicaoLucros.Ioc.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();


            var dbUrl = configuration.GetConnectionString("FirebaseDb");
            var dbCredential = configuration["FirebaseCredential"];

            services.AddScoped(fb => new FirebaseClient(dbUrl, new FirebaseOptions { AuthTokenAsyncFactory = () => Task.FromResult(dbCredential) }));
        }
    }
}
