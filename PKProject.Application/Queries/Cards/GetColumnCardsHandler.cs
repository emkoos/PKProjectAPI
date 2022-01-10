using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Cards
{
    public class GetColumnCardsHandler : IRequestHandler<GetColumnCardsQuery, IEnumerable<Card>>
    {
        private readonly ICardRepository _repository;

        public GetColumnCardsHandler(ICardRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Card>> Handle(GetColumnCardsQuery request, CancellationToken cancellationToken)
        {
            var cards = await _repository.GetCardsByColumnId(request.ColumnId);
            foreach (var card in cards)
            {
                card.DeadlineDate = card.DeadlineDate.Date;
            }

            return cards;
        }
    }
}
