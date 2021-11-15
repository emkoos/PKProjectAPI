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
    public class GetStatusHandler : IRequestHandler<GetStatusQuery, Status>
    {
        private readonly IStatusRepository _repository;

        public GetStatusHandler(IStatusRepository repository)
        {
            _repository = repository;
        }

        public async Task<Status> Handle(GetStatusQuery request, CancellationToken cancellationToken)
        {
            var status = await _repository.GetStatusById(request.Id);

            return status;
        }
    }
}
