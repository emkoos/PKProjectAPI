using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.DTO.Users
{
    public class GetUserDto
    {
        public string Email { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte[] Photo { get; set; }
    }
}
