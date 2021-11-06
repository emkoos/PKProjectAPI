using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.Models
{
    public class Status
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Card> Cards { get; set; }
            = new List<Card>();
    }
}
