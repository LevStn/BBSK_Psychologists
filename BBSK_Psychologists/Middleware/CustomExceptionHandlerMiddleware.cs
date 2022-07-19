using BBSK_Psycho.BusinessLayer.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Net;
using System.Text.Json;
using DataException = BBSK_Psycho.BusinessLayer.Exceptions.DataException;

namespace BBSK_Psycho.Middleware
{
    public class CustomExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;

        public CustomExceptionHandlerMiddleware(RequestDelegate next) =>
            _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (EntityNotFoundException exception)
            {
                await HandleExceptionAsync(context, exception);
            }
            catch (UniquenessException exception)
            {
                await HandleExceptionAsync(context, exception);
            }
            catch (DataException exception)
            {
                await HandleExceptionAsync(context, exception);
            }
            catch (AccessException exception)
            {
                await HandleExceptionAsync(context, HttpStatusCode.Accepted, exception.Message);
            }

        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCode statusCode, string message)
        {
            var code = HttpStatusCode.InternalServerError;
            var result = string.Empty;

            //switch (exception)
            //{
            //    case ValidationException validationException:
            //        code = HttpStatusCode.BadRequest;
            //        result = JsonSerializer.Serialize(validationException.Message);
            //        break;
            //    case EntityNotFoundException:
            //        code = HttpStatusCode.NotFound;
            //        break;
            //    case UniquenessException:
            //        code = HttpStatusCode.UnprocessableEntity;
            //        break;
            //    case DataException:
            //        code = HttpStatusCode.UnprocessableEntity;
            //        break;
            //    case AccessException:
            //        code = HttpStatusCode.Forbidden;
            //        break;

            //}

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            if (result == string.Empty)
            {
                result = JsonSerializer.Serialize(new { error = message });
            }

            return context.Response.WriteAsync(result);
        }
    }
}
