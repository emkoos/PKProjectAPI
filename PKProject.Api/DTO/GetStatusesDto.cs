using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.DTO
{
    public class GetStatusesDto
    {
        public IEnumerable<GetStatusDto> Statuses { get; set; }
    }
}
