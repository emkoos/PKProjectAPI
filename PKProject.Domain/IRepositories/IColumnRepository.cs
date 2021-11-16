using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.IRepositories
{
    public interface IColumnRepository
    {
        Task<bool> ColumnExist(Guid id);
        Task<Column> GetColumnById(Guid id);
        Task<IEnumerable<Column>> GetColumnsByBoardId(Guid boardId);
        Task<bool> CreateColumn(Column model);
        Task<bool?> UpdateColumn(Column model);
        Task<bool> DeleteColumn(Guid id);
    }
}
