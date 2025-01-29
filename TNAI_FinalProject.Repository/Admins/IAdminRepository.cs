using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI_FinalProject.Model.Entities;

namespace TNAI_FinalProject.Repository.Admins
{
    public interface IAdminRepository
    {
        Task<Admin?> GetAdminByIdAsync(int id);
        Task<List<Admin>> GetAllAdminsAsync();
        Task<bool> SaveAdminAsync(Admin admin);
        Task<bool> DeleteAdminAsync(int id);
        Task<bool> CheckUserAdminAsync(int id, User user);
        Task<bool> AddUserToAdminAsync(int  id, User user);
        Task<bool> RemoveUserFromAdminAsync(int id, User user);
        //Task<bool> GetDetailsAsync(int id); do przemyslenia-> UserDetails
        Task<bool> ChangeUserRoleAsync(int id, User user, RoleUser role);
        Task<List<User>> GetAllAdminUsersAsync(int id);
    }
}
