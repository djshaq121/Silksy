using SilksyAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.Interface
{
    public interface IUserRepository
    {
        Task<StoreUser> GetUserByUsernameAsync(string username);

        Task<bool> SaveAllAsync();

    }
}
