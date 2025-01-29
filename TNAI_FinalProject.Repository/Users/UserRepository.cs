using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI_FinalProject.Model;
using TNAI_FinalProject.Model.Entities;

namespace TNAI_FinalProject.Repository.Users
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        public UserRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<User?> GetUserByIdAsync(int id)
        {
            var user = await DbContext.Users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Id == id);

            return user;
        }
        public async Task<List<User>> GetAllUsersAsync()
        {
            var users = await DbContext.Users.Include(x => x.Role).ToListAsync();

            return users;
        }
        public async Task<bool> SaveUserAsync(User user)
        {
            if (user == null)
                return false;

            DbContext.Entry(user).State = user.Id == default(int) ? EntityState.Added : EntityState.Modified;

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await GetUserByIdAsync(id);

            if (user == null) 
                return true;

            DbContext.Users.Remove(user);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch(Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> EmailExistAsync(string email)
        {
            return await DbContext.Users.AnyAsync(x => x.Email == email);
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            var user = await DbContext.Users.Include(x => x.Role).SingleOrDefaultAsync(x => x.Email == email);

            return user;
        }
    }
}
