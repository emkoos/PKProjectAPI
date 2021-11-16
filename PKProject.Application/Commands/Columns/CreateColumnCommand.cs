using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Columns
{
    public class CreateColumnCommand : IRequest<bool>
    {
        public string Title { get; set; }
        public int Position { get; set; }
        public Guid BoardId { get; set; }
    }
}
