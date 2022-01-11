using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Teams
{
    public class CreateTeamCommand : IRequest<bool>
    {
        public string Name { get; set; }
    }
}
