using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.Models
{
    public class Comment
    {
        [Key]
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public User User { get; set; }
        public Guid CardId { get; set; }
        public Card Card { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }
    }
}
