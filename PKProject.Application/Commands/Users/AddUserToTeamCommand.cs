using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Users
{
    public class AddUserToTeamCommand : IRequest<bool>
    {
        public Guid TeamId { get; set; }
        public string UserEmail { get; set; }
    }
}
