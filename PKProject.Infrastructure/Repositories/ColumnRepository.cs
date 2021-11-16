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
    public class ColumnRepository : IColumnRepository
    {
        private readonly AppDbContext _context;

        public ColumnRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> ColumnExist(Guid id)
        {
            return await _context.Columns.AnyAsync(c => c.Id == id);
        }

        public async Task<Column> GetColumnById(Guid id)
        {
            return await _context.Columns.SingleOrDefaultAsync(x => x.Id == id);
        }

        public async Task<IEnumerable<Column>> GetColumnsByBoardId(Guid boardId)
        {
            return await _context.Columns.Where(x => x.BoardId == boardId).ToListAsync();
        }

        public async Task<bool> CreateColumn(Column model)
        {
            await _context.Columns.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> UpdateColumn(Column model)
        {
            var column = await _context.Columns.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (column is null)
            {
                return null;
            }

            _context.Entry(column).CurrentValues.SetValues(model);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteColumn(Guid id)
        {
            var column = await _context.Columns.FirstOrDefaultAsync(x => x.Id == id);
            _context.Columns.Remove(column);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
