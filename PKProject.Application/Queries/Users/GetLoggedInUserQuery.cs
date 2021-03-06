using MediatR;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Users
{
    public class GetLoggedInUserQuery : IRequest<User>
    {
        public string UserEmail { get; set; }
    }
}
