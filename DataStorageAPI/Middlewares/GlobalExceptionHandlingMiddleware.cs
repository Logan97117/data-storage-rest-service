using DataStorageAPI.BaseModels;
using DataStorageAPI.Exceptions;
using System.Net;
using System.Text.Json;

namespace DataStorageAPI.Middlewares
{
    /// <summary>
    /// Global exception handler middleware
    /// </summary>
    public class GlobalExceptionHandlingMiddleware : IMiddleware
    {
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                await next(context).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                context.Response.ContentType = "application/json";
                HttpStatusCode errorStatusCode = GetHttpStatusCode(ex);
                context.Response.StatusCode = (int)errorStatusCode;
                Error errorResponse = new Error()
                {
                    Message = ex.Message,
                    StatusCode = errorStatusCode,
                };
                string errorJson = JsonSerializer.Serialize(errorResponse);
                await context.Response.WriteAsync(errorJson).ConfigureAwait(false);
            }
        }

        /// <summary>
        /// Gets HttpStatusCode based on exception.
        /// </summary>
        /// <param name="ex">Exception type.</param>
        /// <returns>HttpStatusCode</returns>
        private static HttpStatusCode GetHttpStatusCode(Exception ex)
        {
            if (ex is NotFoundException)
            {
                return HttpStatusCode.NotFound;
            }

            if(ex is ArguementException || ex is ArguementNullException)
            {
                return HttpStatusCode.BadRequest;   
            }

            return HttpStatusCode.InternalServerError;
        }
    }
}
