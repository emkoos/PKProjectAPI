using MediatR;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Cards
{
    public class GetColumnCardsQuery : IRequest<IEnumerable<Card>>
    {
        public Guid ColumnId { get; set; }
    }
}
