using MediatR;
using PKProject.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Comments
{
    public class DeleteCommentCommandHandler : IRequestHandler<DeleteCommentCommand, bool>
    {
        private readonly ICommentRepository _repository;

        public DeleteCommentCommandHandler(ICommentRepository repository)
        {
            _repository = repository;
        }

        public async Task<bool> Handle(DeleteCommentCommand request, CancellationToken cancellationToken)
        {
            if (!await _repository.CommentExist(request.Id))
            {
                throw new Exception("Not Found Comment");
            }

            var result = await _repository.DeleteComment(request.Id);

            return result;
        }
    }
}
