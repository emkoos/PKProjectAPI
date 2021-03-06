using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.DTO
{
    public class GetCardDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserEmail { get; set; }
        public Guid ColumnId { get; set; }
        public Guid StatusId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedStatusDoneDate { get; set; }
        public string DeadlineDate { get; set; }
        public int Priority { get; set; }
        public int Estimate { get; set; }
        public byte[] Attachement { get; set; }
    }
}
