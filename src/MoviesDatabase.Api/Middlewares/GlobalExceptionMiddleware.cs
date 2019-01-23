using System;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using MoviesDatabase.Api.Http;
using MoviesDatabase.Api.Http.Errors;
using MoviesDatabase.Core.Logging;

namespace MoviesDatabase.Api.Middlewares
{
    public class GlobalExceptionMiddleware
    {
        private readonly RequestDelegate next;
        private readonly ILogger logger;

        public GlobalExceptionMiddleware(RequestDelegate next, ILogger logger)
        {
            this.next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await this.next(context);
            }
            catch (Exception ex)
            {
                this.logger.LogError(ex);

                await this.HandleExceptionAsync(context);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context)
        {
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var error = new ErrorDetails
            {
                StatusCode = context.Response.StatusCode,
                Message = "An unhandled error occured."
            };

            await context.Response.WriteJsonAsync(error);
        }
    }
}
