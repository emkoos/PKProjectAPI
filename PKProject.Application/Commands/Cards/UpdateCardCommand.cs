using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Application.Commands.Cards
{
    public class UpdateCardCommand : IRequest<bool?>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string UserEmail { get; set; }
        public Guid BoardId { get; set; }
        public Guid StatusId { get; set; }
        public DateTime DeadlineDate { get; set; }
        public int Priority { get; set; }
        public int Estimate { get; set; }
        public byte[] Attachement { get; set; }
    }
}
