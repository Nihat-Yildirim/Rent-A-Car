using Core.Exceptions.Abstract;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Core.Middlewares
{
    public class GlobalExceptionHandlingMiddleware
    {
        RequestDelegate _next;

        public GlobalExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ExceptionBase exception)
            {
                await HandleExceptionAsync(context, exception.Message,exception.StackTrace,exception.StatusCode);
            }
            catch(Exception exception)
            {
                await HandleExceptionAsync(context, exception.Message, exception.StackTrace);
            }
        }

        private Task HandleExceptionAsync(HttpContext context,string message,string? stackTrace,
            HttpStatusCode httpStatusCode = HttpStatusCode.InternalServerError)
        {
            HttpStatusCode statusCode = httpStatusCode;

            if(statusCode == default)
                statusCode = HttpStatusCode.InternalServerError;

            var resultException = JsonSerializer.Serialize(new
            {
                Title = "Beklenmeyen Hata Oluştu !!",
                Message = message,
                StatuCode = (int)statusCode
            });

            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";

            return context.Response.WriteAsync(resultException);
        }
    }
}