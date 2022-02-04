using System;
using System.Net;

namespace PKProject.Domain.Exceptions.ErrorHandlingMiddleware.ErrorModels
{
    public class ErrorDetails : Exception
    {
        public string ExceptionMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
