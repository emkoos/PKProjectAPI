using Microsoft.EntityFrameworkCore;
using PKProject.Domain.IRepositories;
using PKProject.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> TeamExist(Guid id)
        {
            return await _context.Teams.AnyAsync(c => c.Id == id);
        }
    }
}
