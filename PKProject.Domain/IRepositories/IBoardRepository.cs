using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.IRepositories
{
    public interface IBoardRepository
    {
        Task<bool> BoardExist(Guid id);
        Task<Board> GetBoardById(Guid id);
        Task<IEnumerable<Board>> GetBoardsByTeamId(Guid boardId);
        Task<bool> CreateBoard(Board model);
        Task<bool?> UpdateBoard(Board model);
        Task<bool> DeleteBoard(Guid id);
    }
}
