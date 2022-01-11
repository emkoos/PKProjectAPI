using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Users
{
    public class UpdateCardUserCommand : IRequest<bool?>
    {
        public Guid cardId { get; set; }
        public string UserEmail { get; set; }
    }
}
