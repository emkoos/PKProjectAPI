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
    public class GetCommentHandler : IRequestHandler<GetCommentQuery, Comment>
    {
        private readonly ICommentRepository _repository;

        public GetCommentHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Comment> Handle(GetCommentQuery request, CancellationToken cancellationToken)
        {
            var comment = await _repository.GetCommentById(request.Id);

            return comment;
        }
    }
}
