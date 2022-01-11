using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.IRepositories
{
    public interface ITeamRepository
    {
        Task<bool> TeamExist(Guid id);
        Task<IEnumerable<Team>> GetLoggedInUserTeams(string email);
        Task<IEnumerable<User>> GetUsersByTeamId(Guid teamId);
    }
}
