using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.IRepositories
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllStatuses();
        Task<Status> GetStatusById(Guid id);
    }
}
