using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.DTO
{
    public class GetTeamsDto
    {
        public IEnumerable<GetTeamDto> Teams { get; set; }
    }
}
