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
    public class BoardTypeRepository : IBoardTypeRepository
    {
        private readonly AppDbContext _context;

        public BoardTypeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<BoardType>> GetAllBoardTypes()
        {
            return await _context.BoardTypes.ToListAsync();
        }

        public async Task<BoardType> GetBoardTypeById(Guid id)
        {
            return await _context.BoardTypes.Where(x => x.Id == id).SingleOrDefaultAsync();
        }
    }
}
