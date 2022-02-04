using System.Net;

namespace PKProject.Domain.Exceptions.AppExceptions
{
    public class BadRequestException : ResponseException
    {
        public BadRequestException(string errorMessage) : base(HttpStatusCode.BadRequest, errorMessage)
        {

        }
    }
}
