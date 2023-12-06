using ToDo_App_core.Database;
using ToDo_App_core.Repository.User;

namespace ToDo_App_core.Services.User
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<bool> CreateUserAsync(string name)
        {
            using var dbContext = new TdDbContext();
            var result = _userRepository.CreateUserAsync(dbContext, name) != null;
            await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<string>> GetUserNamesAsync()
        {
            using var dbContext = new TdDbContext();
            return (await _userRepository.GetUsersAsync(dbContext))
                .Select(x => x.Name);
        }

        public async Task<bool> RemoveUserAsync(string name)
        {
            using var dbContext = new TdDbContext();
            var result = await _userRepository.DeleteUserAsync(dbContext, name);
            await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<bool> UpdateUserAsync(string oldName, string newName)
        {
            using var dbContext = new TdDbContext();
            var user = await _userRepository.GetUserAsync(dbContext, oldName);
            if (user == null)
                return false;
            user.Name = newName;
            _userRepository.UpdateUser(dbContext, user);
            await dbContext.SaveChangesAsync();
            return true;
        }
    }
}
