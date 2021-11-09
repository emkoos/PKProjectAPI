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
    public class CardRepository : ICardRepository
    {
        private readonly AppDbContext _context;

        public CardRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CardExist(Guid id)
        {
            return await _context.Cards.AnyAsync(c => c.Id == id);
        }
    }
}
