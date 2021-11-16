using MediatR;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Boards
{
    public class GetTeamBoardsQuery : IRequest<IEnumerable<Board>>
    {
        public Guid TeamId { get; set; }
    }
}
