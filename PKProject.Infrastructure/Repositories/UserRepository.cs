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

        public async Task<bool> AddUserToTeam(string email, Guid teamId)
        {
            var result = await _context.UsersTeams.FirstOrDefaultAsync(x => x.UserEmail == email && x.TeamId == teamId);
            if (result != null)
            {
                return false;
            }

            var model = new UsersTeam
            {
                UserEmail = email,
                TeamId = teamId
            };

            await _context.UsersTeams.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> CreateUser(User user)
        {
            await _context.Users.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var editUser = await _context.Users.FirstOrDefaultAsync(x => x.Email == user.Email);
            if (editUser is null)
            {
                return false;
            }

            _context.Entry(editUser).CurrentValues.SetValues(user);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UserExist(string email)
        {
            return await _context.Users.AnyAsync(e => e.Email == email);
        }

        public async Task<bool> UserExistInTeam(string email, Guid teamId)
        {
            var result = await _context.UsersTeams.FirstOrDefaultAsync(x => x.UserEmail == email && x.TeamId == teamId);
            if (result != null)
            {
                return true;
            }
            return false;
        }
    }
}
