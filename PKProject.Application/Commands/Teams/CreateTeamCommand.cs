using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Teams
{
    public class CreateTeamCommand : IRequest<bool>
    {
        public string Name { get; set; }
        [JsonIgnore]
        public string UserEmail { get; set; }
    }
}
