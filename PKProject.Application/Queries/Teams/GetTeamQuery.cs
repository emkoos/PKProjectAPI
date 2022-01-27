using MediatR;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Teams
{
    public class GetTeamQuery : IRequest<Team>
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
