using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Comments
{
    public class UpdateCommentCommand : IRequest<bool?>
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public Guid CardId { get; set; }
        public string Content { get; set; }
    }
}
