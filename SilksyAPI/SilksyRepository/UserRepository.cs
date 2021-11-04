using Microsoft.EntityFrameworkCore;
using SilksyAPI.Data;
using SilksyAPI.Entities;
using SilksyAPI.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SilksyAPI.SilksyRepository
{
    public class UserRepository : IUserRepository
    {
        private readonly SilksyContext context;

        public UserRepository(SilksyContext context)
        {
            this.context = context;
        }

        public async Task<StoreUser> GetUserByUsernameAsync(string username)
        {
           return await context.Users.SingleOrDefaultAsync(user => user.UserName == username);
        }

        public async Task<bool> SaveAllAsync()
        {
            return await context.SaveChangesAsync() > 0;
        }
    }
}
