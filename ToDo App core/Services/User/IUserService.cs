namespace ToDo_App_core.Services.User
{
    public interface IUserService
    {
        Task<bool> CreateUserAsync(string name);
        Task<bool> RemoveUserAsync(string name);
        Task<bool> UpdateUserAsync(string oldName, string newName);
        Task<IEnumerable<string>> GetUserNamesAsync();
    }
}
