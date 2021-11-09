using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Comments
{
    public class GetUserCommentsHandler : IRequestHandler<GetUserCommentsQuery, IEnumerable<Comment>>
    {
        private readonly ICommentRepository _repository;

        public GetUserCommentsHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Comment>> Handle(GetUserCommentsQuery request, CancellationToken cancellationToken)
        {
            var comments = await _repository.GetCommentsByUserEmail(request.Email);

            return comments;
        }
    }
}
