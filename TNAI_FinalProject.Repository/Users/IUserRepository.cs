using TNAI_FinalProject.Model.Entities;

namespace TNAI_FinalProject.Repository.Users
{
    public interface IUserRepository
    {
        Task<User?> GetUserByIdAsync(int id);
        Task<List<User>> GetAllUsersAsync();
        Task<bool> SaveUserAsync(User user);
        Task<bool> DeleteUserAsync(int id);
        Task<bool> EmailExistAsync(string email);
    }
}
