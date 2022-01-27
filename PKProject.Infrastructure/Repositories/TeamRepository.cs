using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PKProject.Domain.IRepositories;
using PKProject.Domain.Models;
using PKProject.Infrastructure.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Infrastructure.Repositories
{
    public class TeamRepository : ITeamRepository
    {
        private readonly AppDbContext _context;

        public TeamRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<bool> CreateTeam(Team model, string email)
        {
            await _context.Teams.AddAsync(model);

            var userTeams = new UsersTeam
            {
                UserEmail = email,
                TeamId = model.Id
            };

            await _context.UsersTeams.AddAsync(userTeams);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> DeleteTeam(Guid teamId)
        {
            var team = await _context.Teams.FirstOrDefaultAsync(x => x.Id == teamId);
            _context.Teams.Remove(team);

            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<IEnumerable<Team>> GetLoggedInUserTeams(string email)
        {
            var userTeams = await _context.UsersTeams.Where(x => x.UserEmail == email).ToListAsync();

            var teams = new List<Team>();

            foreach (var item in userTeams)
            {
                var team = await _context.Teams.Where(x => x.Id == item.TeamId).FirstOrDefaultAsync();
                teams.Add(team);
            }

            return teams;
        }

        public async Task<Team> GetTeamById(Guid teamId)
        {
            return await _context.Teams.FirstOrDefaultAsync(x => x.Id == teamId);
        }

        public async Task<IEnumerable<User>> GetUsersByTeamId(Guid teamId)
        {
            var userTeams = await _context.UsersTeams.Where(x => x.TeamId == teamId).ToListAsync();
            
            var users = new List<User>();

            foreach (var item in userTeams)
            {
                var user = await _context.Users.Where(x => x.Email == item.UserEmail).FirstOrDefaultAsync();
                users.Add(user);
            }

            return users;
        }

        public async Task<bool> TeamExist(Guid id)
        {
            return await _context.Teams.AnyAsync(c => c.Id == id);
        }

      
    }
}
