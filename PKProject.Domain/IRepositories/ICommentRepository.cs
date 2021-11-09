using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.IRepositories
{
    public interface ICommentRepository
    {
        Task<IEnumerable<Comment>> GetCommentsByUserEmail(string email);
        Task<IEnumerable<Comment>> GetCommentsByCardId(Guid id);
        Task<Comment> GetCommentById(Guid id);

        Task<bool> CreateComment(Comment model);
    }
}
