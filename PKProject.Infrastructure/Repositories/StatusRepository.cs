using Microsoft.EntityFrameworkCore;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using PKProject.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Infrastructure.Repositories
{
    public class StatusRepository : IStatusRepository
    {
        private readonly AppDbContext _context;

        public StatusRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Status>> GetAllStatuses()
        {
            return await _context.Statuses.ToListAsync();
        }

        public async Task<Status> GetStatusById(Guid id)
        {
            return await _context.Statuses.SingleOrDefaultAsync(x => x.Id == id);
        }
    }
}
