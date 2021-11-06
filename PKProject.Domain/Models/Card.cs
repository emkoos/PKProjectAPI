using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.Models
{
    public class Card
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserEmail { get; set; }
        public User User { get; set; }
        public Guid BoardId { get; set; }
        public Board Board { get; set; }
        public ICollection<Comment> Comments { get; set; }
            = new List<Comment>();

        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime DeadlineDate { get; set; }
        public int Priority { get; set; }
        public int Estimate { get; set; }
        public byte[] Attachement { get; set; }
    }
}
