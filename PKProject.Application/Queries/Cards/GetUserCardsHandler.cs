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
    public class GetUserCardsHandler : IRequestHandler<GetUserCardsQuery, IEnumerable<Card>>
    {
        private readonly ICardRepository _repository;

        public GetUserCardsHandler(ICardRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Card>> Handle(GetUserCardsQuery request, CancellationToken cancellationToken)
        {
            var cards = await _repository.GetCardsByUserEmail(request.Email);

            return cards;
        }
    }
}
