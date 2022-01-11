using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Users
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte[] Photo { get; set; }
    }
}
