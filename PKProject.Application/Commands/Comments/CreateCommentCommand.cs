using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Comments
{
    public class CreateNewCommentCommand
    {
        public Guid CardId { get; set; }
        public string Content { get; set; }
    }

    public class CreateCommentCommand : IRequest<bool>
    {
        public string UserEmail { get; set; }
        public Guid CardId { get; set; }
        public string Content { get; set; }
    }
}
