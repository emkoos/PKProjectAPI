using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Columns
{
    public class DeleteColumnCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
    }
}
