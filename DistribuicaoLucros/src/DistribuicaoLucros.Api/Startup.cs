using System;
using DistribuicaoLucros.Api.Extensions;
using DistribuicaoLucros.Api.Filters;
using DistribuicaoLucros.Domain.Notification;
using DistribuicaoLucros.Ioc.DependencyInjection;
using DistribuicaoLucros.Ioc.Mapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Rewrite;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace DistribuicaoLucros.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            ConfigureTools.ConfigureDependenciesTools(services);
            ConfigureService.ConfigureDependenciesService(services);
            ConfigureRepository.ConfigureDependenciesService(services, Configuration);
            AutoMapperConfig.ConfigureMappers(services);
            GlobalExceptionHandlerExtensions.AddGlobalExceptionHandler(services);

            services.AddScoped<NotificationContext>();

            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1",
                    new OpenApiInfo
                    {
                        Title = "Profit distribution",
                        Version = "v1",
                        Description = "",
                        Contact = new OpenApiContact
                        {
                            Name = "Patrick Vianna",
                            Email = "patrickviannapblv@gmail.com",
                            Url = new Uri("https://github.com/patrickvianna")
                        }
                    });
            });

            services.AddControllers(options => options.Filters.Add<NotificationFilter>());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            GlobalExceptionHandlerExtensions.UseGlobalExceptionHandler(app);

            //Ativando middlewares para uso do Swagger
            app.UseSwagger();
            app.UseSwaggerUI(c => {
                c.RoutePrefix = string.Empty;
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Projeto em AspNetCore 3.1");
            });

            // Redireciona o Link para o Swagger, quando acessar a rota principal
            var option = new RewriteOptions();
            option.AddRedirect("^$", "swagger");
            app.UseRewriter(option);

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
