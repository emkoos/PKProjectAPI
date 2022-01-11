using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.IServices
{
    public interface IEmailSender
    {
        Task<bool> SendEmail(string subject, string toUserEmail, string text);
    }
}
