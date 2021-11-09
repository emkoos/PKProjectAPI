using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Comments
{
    public class GetCardCommentsHandler : IRequestHandler<GetCardCommentsQuery, IEnumerable<Comment>>
    {
        private readonly ICommentRepository _repository;

        public GetCardCommentsHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Comment>> Handle(GetCardCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _repository.GetCommentsByCardId(request.CardId);

            return comments;
        }
    }
}
