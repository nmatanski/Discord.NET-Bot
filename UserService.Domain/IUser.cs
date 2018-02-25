using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using UserService.Domain.Models;

namespace UserService.Domain
{
    public interface IUser
    {
        Task RegisterAsync(User user);
        Task<User> GetUserByIdAsync(string id);
        Task<User> GetUserByEmailAsync(string email);
        Task<List<User>> GetAllUsersAsync();
    }
}
