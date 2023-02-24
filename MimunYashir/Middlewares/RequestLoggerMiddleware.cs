using MimunYashirInfrastructure.Exceptions;
using MimunYashirInfrastructure.Log;

namespace MimunYashir.Middlewares
{
    public class RequestLoggerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IAppLogger<RequestLoggerMiddleware> logger;

        public RequestLoggerMiddleware(AppGeneralLogger<RequestLoggerMiddleware> logger, RequestDelegate next)
        {
            _next = next;
            this.logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (NotAuthorizedException ex)
            {
                logger.Error(new ManagedException(ex, $"General error - NotAuthorizedException: {ex.Message}", AppModule.GENERAL_HANDLER, AppLayer.WEB_API));
                context.Response.StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status403Forbidden;
                await context.Response.WriteAsync($"Forbidden");
            }

            catch (Exception ex)
            {
                logger.Error(new ManagedException(ex, $"General error: {ex.Message}", AppModule.GENERAL_HANDLER, AppLayer.WEB_API));
                throw new Exception(ex.Message + Environment.NewLine + ex.StackTrace);
            }

        }
    }
}
