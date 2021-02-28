using AutoMapper;
using DistribuicaoLucros.Domain.Mappers;
using Microsoft.Extensions.DependencyInjection;

namespace DistribuicaoLucros.Ioc.Mapper
{
    public class AutoMapperConfig
    {
        public static void ConfigureMappers(IServiceCollection services)
        {
            var mappingConfig = new MapperConfiguration(mc => {
                mc.AddProfile(new EmployeeProfile());
                mc.AddProfile(new ProfitDistributionProfile());
            });
            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
