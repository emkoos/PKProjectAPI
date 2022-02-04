using System.Net;

namespace PKProject.Domain.Exceptions.AppExceptions
{
    public class NotFoundException : ResponseException
    {
        public NotFoundException(string errorMessage) : base(HttpStatusCode.NotFound, errorMessage)
        {

        }
    }
}
