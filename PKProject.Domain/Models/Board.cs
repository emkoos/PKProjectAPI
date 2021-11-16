using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.Models
{
    public class Board
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TeamId { get; set; }
        public Team Team { get; set; }
        public Guid BoardTypeId { get; set; }
        public BoardType BoardType { get; set; }
        public ICollection<Column> Columns { get; set; }
            = new List<Column>();
    }
}
