using System;
using System.Net;

namespace PKProject.Domain.Exceptions
{
    public abstract class ResponseException : Exception
    {
        public HttpStatusCode ResponseCode { get;  }

        public ResponseException(HttpStatusCode responseCode, string errorMessage) : base(errorMessage)
        {
            ResponseCode = responseCode;
        }
    }
}
