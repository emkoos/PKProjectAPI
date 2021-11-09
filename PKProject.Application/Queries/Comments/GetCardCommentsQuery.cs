using MediatR;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Comments
{
    public class GetCardCommentsQuery : IRequest<IEnumerable<Comment>>
    {
        public Guid CardId { get; set; }
    }
}
