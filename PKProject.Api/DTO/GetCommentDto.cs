using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PKProject.Api.DTO
{
    public class GetCommentDto
    {
        public Guid Id { get; set; }
        public string UserEmail { get; set; }
        public Guid CardId { get; set; }
        public string Content { get; set; }
        public string Date { get; set; }
    }
}
