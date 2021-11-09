using MediatR;
using PKProject.Domain.Models;
using System;

namespace PKProject.Application.Queries.Comments
{
    public class GetCommentQuery : IRequest<Comment>
    {
        public Guid Id { get; set; }
    }
}
