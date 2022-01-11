using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string Username { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public byte[] Photo { get; set; }
        public ICollection<UsersTeam> UserTeams { get; set; }
            = new List<UsersTeam>();
        public ICollection<Comment> Comments { get; set; }
            = new List<Comment>();
        public ICollection<Card> Cards { get; set; }
            = new List<Card>();
    }
}
