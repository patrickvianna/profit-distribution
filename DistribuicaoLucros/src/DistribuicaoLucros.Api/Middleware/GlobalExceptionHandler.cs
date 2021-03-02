using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace DistribuicaoLucros.Api.Middleware
{
    public class GlobalExceptionHandlerMiddleware : IMiddleware
    {
        private readonly ILogger<GlobalExceptionHandlerMiddleware> logger;

        public GlobalExceptionHandlerMiddleware(ILogger<GlobalExceptionHandlerMiddleware> logger)
        {
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context);
            }
            catch (Exception ex)
            {
                this.logger.LogCritical($"Ocorreu erro {ex}");
                await this.HandlerAsync(context, ex);
            }
        }

        private async Task HandlerAsync(HttpContext context, Exception ex)
        {
            var problemDetails = new ProblemDetails();

            if (ex is ArgumentException badHttpRequestException)
            {
                problemDetails.Title = ex.Message;
                context.Response.StatusCode = StatusCodes.Status400BadRequest;
            }
            else
            {
                problemDetails.Title = "Erro de sistema";
                context.Response.StatusCode = StatusCodes.Status500InternalServerError;
            }

            context.Response.ContentType = "application/problem+json";


            var json = JsonConvert.SerializeObject(problemDetails.Title);

            await context.Response.WriteAsync(json);
        }
    }
}
