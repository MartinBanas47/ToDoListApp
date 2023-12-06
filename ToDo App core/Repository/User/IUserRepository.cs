using ToDo_App_core.Database;
using ToDo_App_core.Database.Entity;

namespace ToDo_App_core.Repository.User
{
    public interface IUserRepository
    {
        Task<UserEntity> CreateUserAsync(TdDbContext context, string name);
        Task<bool> DeleteUserAsync(TdDbContext context, int userId);
        Task<bool> DeleteUserAsync(TdDbContext context, string name);
        Task<UserEntity?> GetUserAsync(TdDbContext context, int userId);
        Task<UserEntity?> GetUserAsync(TdDbContext context, string name);
        Task<IEnumerable<UserEntity>> GetUsersAsync(TdDbContext context);
        UserEntity UpdateUser(TdDbContext context, UserEntity user);
    }
}
