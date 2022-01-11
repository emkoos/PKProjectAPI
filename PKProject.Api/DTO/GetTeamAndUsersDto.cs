using PKProject.Domain.Models;
using System;
using System.Collections.Generic;

namespace PKProject.Api.DTO
{
    public class GetTeamAndUsersDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<User> Users { get; set; }
    }
}
