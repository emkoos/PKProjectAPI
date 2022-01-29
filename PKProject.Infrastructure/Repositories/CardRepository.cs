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
        

        public async Task<Card> GetCardById(Guid id)
        {
            return await _context.Cards.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Card>> GetCardsByColumnId(Guid id)
        {
            return await _context.Cards.Where(x => x.ColumnId == id).ToListAsync();
        }

        public async Task<IEnumerable<Card>> GetCardsByUserEmail(string email)
        {
            return await _context.Cards.Where(x => x.UserEmail == email).ToListAsync();
        }

        public async Task<IEnumerable<Card>> GetCardsByDeadlineDateIn24H(DateTime date)
        {
            return await _context.Cards.Where(x => x.DeadlineDate.Date == date.Date && x.DeadlineDate.Hour == date.Hour && x.DeadlineDate.Minute == date.Minute).ToListAsync();
            //return await _context.Cards.Where(x => x.DeadlineDate == date).ToListAsync();
        }

        public async Task<bool> CreateCard(Card model)
        {
            await _context.Cards.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> UpdateCard(Card model)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (card is null)
            {
                return null;
            }

            _context.Entry(card).CurrentValues.SetValues(model);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteCard(Guid id)
        {
            var card = await _context.Cards.FirstOrDefaultAsync(x => x.Id == id);
            _context.Cards.Remove(card);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
