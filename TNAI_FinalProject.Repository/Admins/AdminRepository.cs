using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI_FinalProject.Model;
using TNAI_FinalProject.Model.Entities;
using TNAI_FinalProject.Repository.Users;

namespace TNAI_FinalProject.Repository.Admins
{
    public class AdminRepository : BaseRepository, IAdminRepository
    {
        public AdminRepository(AppDbContext dbContext) : base(dbContext)
        {
        }


        public async Task<Admin?> GetAdminByIdAsync(int id)
        {
            var admin = await DbContext.Admins.SingleOrDefaultAsync(x => x.Id == id);

            return admin;
        }

        public async Task<List<Admin>> GetAllAdminsAsync()
        {
            var admins = await DbContext.Admins.ToListAsync();

            return admins;
        }

        public async Task<bool> SaveAdminAsync(Admin admin)
        {
            if (admin == null)
                return false;

            DbContext.Entry(admin).State = admin.Id == default(int) ? EntityState.Added : EntityState.Modified;

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

        public async Task<bool> DeleteAdminAsync(int id)
        {
            var admin = await GetAdminByIdAsync(id);

            if (admin == null)
                return true;

            DbContext.Admins.Remove(admin);

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

        public async Task<bool> CheckUserAdminAsync(int id, User user)
        {
            var admin = await GetAdminByIdAsync(id);

            if (admin == null)
                return false;

            bool NotInList = false;

            foreach (var u in admin.Admin_Users)
            {
                if (u.Id == user.Id)
                    NotInList = true;
            }

            return NotInList;
        }

        public async Task<bool> AddUserToAdminAsync(int id, User user)
        {
            var admin = await GetAdminByIdAsync(id);

            if (admin == null)
                return false;

            bool exist = await CheckUserAdminAsync(id, user);

            if (exist)
                return false;
            else
            {

                admin.Admin_Users.Add(user);

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


        public async Task<bool> RemoveUserFromAdminAsync(int id, User user)
        {
            var admin = await GetAdminByIdAsync(id);

            if (admin == null)
                return false;

            bool exists = await CheckUserAdminAsync(id,user);


            if (exists)
            {
                admin.Admin_Users.Remove(user);

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
            else
            {
                return false;
            }

        }

        public async Task<bool> ChangeUserRoleAsync(int id, User user, RoleUser role)
        {
            var admin = await GetAdminByIdAsync(id);

            if (admin == null)
                return false;

            bool exists = await CheckUserAdminAsync (id,user);

            if (exists)
            {
                user.Role = role;

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
            else
            {
                return false;
            }  


        }

    }
}
