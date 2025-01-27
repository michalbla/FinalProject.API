using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TNAI_FinalProject.Model.Entities;

namespace TNAI_FinalProject.Repository.Users
{
    public interface IUserDetailsRepository
    {
        Task<UserDetails> GetUserDetailsByIdAsync(int id);
        Task<List<UserDetails>> GetAllUsersDetailsAsync();
        Task<bool> SaveUserDetailsAsync(UserDetails userDetails);
        Task<bool> DeleteUserDetailsAsync(int id);

    }
}
