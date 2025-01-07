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
    internal class UserDetailsRepository : BaseRepository, IUserDetailsRepository
    {
        public UserDetailsRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
        public async Task<UserDetails> GetUserDetailsByIdAsync(int id)
        {
            var user = await DbContext.UserDetails.Include(x => x.User).SingleOrDefaultAsync(x => x.Id == id);

            return user;
        }
        public async Task<List<UserDetails>> GetAllUsersDetailsAsync()
        {
            var users = await DbContext.UserDetails.Include(x => x.User).ToListAsync();

            return users;
        }
        public async Task<bool> SaveUserDetailsAsync(UserDetails userDetails)
        {
            if (userDetails == null)
                return false;

            DbContext.Entry(userDetails).State = userDetails.Id == default(int) ? EntityState.Added : EntityState.Modified;

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }
        public async Task<bool> DeleteUserDetailsAsync(int id)
        {
            var user = await GetUserDetailsByIdAsync(id);

            if (user == null)
                return true;

            DbContext.UserDetails.Remove(user);

            try
            {
                await DbContext.SaveChangesAsync();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }
    }
}
