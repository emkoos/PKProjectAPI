using System.Net;

namespace PKProject.Domain.Exceptions.AppExceptions
{
    public class NotAvailableException : DomainException
    {
        public NotAvailableException(string errorMessage) : base(HttpStatusCode.Conflict, errorMessage)
        {

        }
    }
}
