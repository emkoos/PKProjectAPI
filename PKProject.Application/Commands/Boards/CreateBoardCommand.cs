using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Boards
{
    public class CreateBoardCommand : IRequest<Guid>
    {
        public string Name { get; set; }
        public Guid TeamId { get; set; }
        public Guid BoardTypeId { get; set; }
    }
}
