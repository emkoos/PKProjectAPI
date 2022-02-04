using System.Net;

namespace PKProject.Domain.Exceptions
{
    public abstract class DomainException : ResponseException
    {
        public DomainException(HttpStatusCode responseCode, string errorMessage) : base(responseCode, errorMessage) { }
    }
}
