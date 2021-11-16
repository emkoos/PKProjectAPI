using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.DTO
{
    public class GetColumnDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int Position { get; set; }
        public Guid BoardId { get; set; }
    }
}
