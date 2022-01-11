using PKProject.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PKProject.Domain.IRepositories
{
    public interface IUserRepository
    {
        Task<bool> UserExist(string email);
        Task<bool> CreateUser(User user);
        Task<bool> UpdateUser(User user);
    }
}
