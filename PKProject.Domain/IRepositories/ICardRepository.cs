using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.IRepositories
{
    public interface ICardRepository
    {
        Task<bool> CardExist(Guid id);
        Task<Card> GetCardById(Guid id);
        Task<IEnumerable<Card>> GetCardsByColumnId(Guid id);
        Task<IEnumerable<Card>> GetCardsByUserEmail(string email);
        Task<bool> CreateCard(Card model);
        Task<bool?> UpdateCard(Card model);
        Task<bool> DeleteCard(Guid id);
    }
}
