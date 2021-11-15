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
    public class BoardRepository : IBoardRepository
    {
        private readonly AppDbContext _context;

        public BoardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> BoardExist(Guid id)
        {
            return await _context.Boards.AnyAsync(c => c.Id == id);
        }
    }
}
