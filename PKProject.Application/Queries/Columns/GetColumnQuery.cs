using MediatR;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Columns
{
    public class GetColumnQuery : IRequest<Column>
    {
        public Guid Id { get; set; }
    }
}
