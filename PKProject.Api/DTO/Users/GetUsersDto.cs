using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.DTO.Users
{
    public class GetUsersDto
    {
        public IEnumerable<GetUserDto> Users { get; set; }
    }
}
