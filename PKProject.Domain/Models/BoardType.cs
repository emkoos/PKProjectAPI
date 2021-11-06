using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.Models
{
    public class BoardType
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public ICollection<Board> Boards { get; set; }
            = new List<Board>();
    }
}
