using System.Net;

namespace PKProject.Domain.Exceptions.ErrorHandlingMiddleware.ErrorModels
{
    public class ResponseDetails
    {
        public string EsceptionMessage { get; set; }
        public HttpStatusCode StatusCode { get; set; }
    }
}
