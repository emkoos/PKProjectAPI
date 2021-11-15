using MediatR;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Cards
{
    public class GetUserCardsQuery : IRequest<IEnumerable<Card>>
    {
        public string Email { get; set; }
    }
}
