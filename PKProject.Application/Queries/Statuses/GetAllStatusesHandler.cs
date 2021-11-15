using MediatR;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace PKProject.Application.Queries.Statuses
{
    public class GetAllStatusesHandler : IRequestHandler<GetAllStatusesQuery, IEnumerable<Status>>
    {
        private readonly IStatusRepository _repository;

        public GetAllStatusesHandler(IStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<Status>> Handle(GetAllStatusesQuery request, CancellationToken cancellationToken)
        {
            var statusesList = await _repository.GetAllStatuses();

            return statusesList;
        }
    }
}
