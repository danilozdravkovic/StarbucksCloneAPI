using FluentValidation;
using StarbucksClone.Application.Exceptions;

namespace StarbuckClone.API.Core
{
    public class GlobalExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
   
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception exception)
            {
                if (exception is UnauthorizedAccessException)
                {
                    httpContext.Response.StatusCode = 401;
                    return;
                }

                if (exception is ValidationException ex)
                {
                    httpContext.Response.StatusCode = 422;
                    var body = ex.Errors.Select(x => new { Property = x.PropertyName, Error = x.ErrorMessage });

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }

                if (exception is NotFoundException)
                {
                    httpContext.Response.StatusCode = 404;
                    return;
                }

                if (exception is ConflictException c)
                {
                    httpContext.Response.StatusCode = 409; ;
                    var body = new { error = c.Message };

                    await httpContext.Response.WriteAsJsonAsync(body);
                    return;
                }

                httpContext.Response.StatusCode = 500;
                Console.Write(exception);
                await httpContext.Response.WriteAsJsonAsync(new { Message = $"An unexpected error has occured. Please contact our support." });
                
            }
        }
    }
}
