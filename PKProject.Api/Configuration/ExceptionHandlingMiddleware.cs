using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using PKProject.Domain.Exceptions;
using PKProject.Domain.Exceptions.ErrorHandlingMiddleware;
using PKProject.Domain.Exceptions.ErrorHandlingMiddleware.ErrorModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.Configuration
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionHandlingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ResponseException ex)
            {
                await WriteExceptionAsync(context, ex.ToErrorDetails(ex.ResponseCode));
            }
            catch (Exception ex)
            {
                await WriteExceptionAsync(context, ex.ToErrorDetails());
            }
        }

        private async Task WriteExceptionAsync(HttpContext context, ErrorDetails details)
        {
            ResponseDetails model = new ResponseDetails();
            model.EsceptionMessage = details.ExceptionMessage;
            model.StatusCode = details.StatusCode;

            string error = JsonConvert.SerializeObject(model, new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            });

            context.Response.StatusCode = (int)details.StatusCode;
            context.Response.ContentType = "application/json";

            await context.Response.WriteAsync(error);
        }
    }
}
