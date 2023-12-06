using Microsoft.EntityFrameworkCore;
using ToDo_App_core.Database;
using ToDo_App_core.Database.Entity;

namespace ToDo_App_core.Repository.User
{
    public class UserRepository : IUserRepository
    {
        public async Task<UserEntity> CreateUserAsync(TdDbContext context, string name)
        {
            var user = new UserEntity { Name = name };
            await context.AddAsync(user);
            return user;
        }

        public async Task<bool> DeleteUserAsync(TdDbContext context, int userId)
        {
            var user = await context.Users.FindAsync(userId);
            if (user == null)
                return false;
            context.Remove(user);
            return true;
        }

        public async Task<bool> DeleteUserAsync(TdDbContext context, string name)
        {
            var user = await context.Users.FirstOrDefaultAsync(u => u.Name == name);
            if (user == null)
                return false;
            context.Remove(user);
            return true;
        }

        public async Task<UserEntity?> GetUserAsync(TdDbContext context, int userId)
        {
            return await context.Users.FindAsync(userId);
        }

        public async Task<UserEntity?> GetUserAsync(TdDbContext context, string name)
        {
            return await context.Users.FirstOrDefaultAsync(u => u.Name == name);
        }

        public async Task<IEnumerable<UserEntity>> GetUsersAsync(TdDbContext context)
        {
            return await context.Users.ToListAsync();
        }

        public UserEntity UpdateUser(TdDbContext context, UserEntity user)
        {
            context.Users.Update(user);
            return user;
        }
    }
}
