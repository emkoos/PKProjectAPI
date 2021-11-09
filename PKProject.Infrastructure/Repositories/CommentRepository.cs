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
    public class CommentRepository : ICommentRepository
    {
        private readonly AppDbContext _context;

        public CommentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Comment>> GetCommentsByUserEmail(string email)
        {
            return await _context.Comments.Where(x => x.UserEmail == email).ToListAsync();
        }

        public async Task<IEnumerable<Comment>> GetCommentsByCardId(Guid id)
        {
            return await _context.Comments.Where(x => x.CardId == id).ToListAsync();
        }

        public async Task<Comment> GetCommentById(Guid id)
        {
            return await _context.Comments.Where(x => x.Id == id).SingleOrDefaultAsync();
        }

        public async Task<bool> CreateComment(Comment model)
        {
            await _context.Comments.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool?> UpdateComment(Comment model)
        {
            var comment = await _context.Comments.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (comment is null)
            {
                return null;
            }

            _context.Entry(comment).CurrentValues.SetValues(model);

            return await _context.SaveChangesAsync() > 0;
        }
    }
}
