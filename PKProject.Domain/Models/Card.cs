using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.Models
{
    public class Card
    {
        [Key]
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserEmail { get; set; }
        public User User { get; set; }
        public Guid ColumnId { get; set; }
        public Column Column { get; set; }
        public ICollection<Comment> Comments { get; set; }
            = new List<Comment>();

        public Guid StatusId { get; set; }
        public Status Status { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedStatusDoneDate { get; set; }
        public DateTime DeadlineDate { get; set; }
        public int Priority { get; set; }
        public int Estimate { get; set; }
        public byte[] Attachement { get; set; }
    }
}
