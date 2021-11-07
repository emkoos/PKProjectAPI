using MediatR;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.BoardTypes
{
    public class GetAllBoardTypesQuery: IRequest<IEnumerable<BoardType>>
    {
    }
}
