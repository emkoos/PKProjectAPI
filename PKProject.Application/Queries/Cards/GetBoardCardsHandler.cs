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
    public class GetBoardCardsHandler : IRequestHandler<GetBoardCardsQuery, IEnumerable<Card>>
    {
        private readonly ICardRepository _repository;

        public GetBoardCardsHandler(ICardRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Card>> Handle(GetBoardCardsQuery request, CancellationToken cancellationToken)
        {
            var cards = await _repository.GetCardsByBoardId(request.BoardId);

            return cards;
        }
    }
}
