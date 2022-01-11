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
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserExist(string email)
        {
            return await _context.Users.AnyAsync(e => e.Email == email);
        }
    }
}
