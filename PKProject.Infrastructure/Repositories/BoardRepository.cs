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

        public async Task<Board> GetBoardById(Guid id)
        {
            return await _context.Boards.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Board>> GetBoardsByTeamId(Guid teamId)
        {
            return await _context.Boards.Where(x => x.TeamId == teamId).ToListAsync();
        }

        public async Task<bool> CreateBoard(Board model)
        {
            await _context.Boards.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> UpdateBoard(Board model)
        {
            var board = await _context.Boards.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (board is null)
            {
                return null;
            }

            _context.Entry(board).CurrentValues.SetValues(model);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteBoard(Guid id)
        {
            var board = await _context.Boards.FirstOrDefaultAsync(x => x.Id == id);
            _context.Boards.Remove(board);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
