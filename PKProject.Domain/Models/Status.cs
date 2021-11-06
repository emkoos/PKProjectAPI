using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.Models
{
    public class Status
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Card> Cards { get; set; }
            = new List<Card>();
    }
}
