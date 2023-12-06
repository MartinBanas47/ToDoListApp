using ToDo_App_core.Database;
using ToDo_App_core.Database.Entity;

namespace ToDo_App_core.Repository.ToDo
{
    public interface IToDoRepository
    {
        Task<ToDoEntity> CreateToDoAsync(TdDbContext context, string text, int userId, DateTime? date);
        Task<bool> DeleteToDoAsync(TdDbContext context, int id);
        Task<ToDoEntity?> GetToDoAsync(TdDbContext context, int id);
        Task<IEnumerable<ToDoEntity>> GetToDosAsync(TdDbContext context);
        Task<IEnumerable<ToDoEntity>> GetToDosForPersonAsync(TdDbContext context, string name);
        ToDoEntity UpdateToDo(TdDbContext context, ToDoEntity entity);
        Task<bool> DeleteOldToDosAsync(TdDbContext context);
    }
}
