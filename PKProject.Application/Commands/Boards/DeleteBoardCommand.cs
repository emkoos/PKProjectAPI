using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Boards
{
    public class DeleteBoardCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
