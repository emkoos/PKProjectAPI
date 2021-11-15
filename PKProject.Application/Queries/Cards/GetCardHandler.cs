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
    public class GetCardHandler : IRequestHandler<GetCardQuery, Card>
    {
        private readonly ICardRepository _repository;

        public GetCardHandler(ICardRepository repository)
        {
            _repository = repository;
        }

        public async Task<Card> Handle(GetCardQuery request, CancellationToken cancellationToken)
        {
            var card = await _repository.GetCardById(request.Id);

            return card;
        }
    }
}
