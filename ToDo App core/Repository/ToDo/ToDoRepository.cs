using Microsoft.EntityFrameworkCore;
using ToDo_App_core.Database;
using ToDo_App_core.Database.Entity;

namespace ToDo_App_core.Repository.ToDo
{
    public class ToDoRepository : IToDoRepository
    {
        public async Task<ToDoEntity> CreateToDoAsync(TdDbContext context, string text, int userId, DateTime? date)
        {
            var todo = new ToDoEntity { Text = text, UserId = userId, Date = date };
            try
            {
                await context.AddAsync(todo);
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Error occured during insert. Make sure the name is unique.", ex);
            }
            return todo;
        }

        public async Task<bool> DeleteOldToDosAsync(TdDbContext context)
        {
            var oldTodos = await context.ToDoEntities.Where(td => td.Date < DateTime.Today).ToArrayAsync();
            context.ToDoEntities.RemoveRange(oldTodos);
            return true;
        }

        public async Task<bool> DeleteToDoAsync(TdDbContext context, int id)
        {
            var todo = await context.ToDoEntities.FindAsync(id);
            if (todo == null)
                return false;
            context.ToDoEntities.Remove(todo);
            return true;
        }

        public async Task<ToDoEntity?> GetToDoAsync(TdDbContext context, int id)
        {
            return await context.ToDoEntities.FindAsync(id);
        }

        public async Task<IEnumerable<ToDoEntity>> GetToDosAsync(TdDbContext context)
        {
            return await context.ToDoEntities
                .Include(td => td.User)
                .ToListAsync();
        }




        public async Task<IEnumerable<ToDoEntity>> GetToDosForPersonAsync(TdDbContext context, string name)
        {
            return await context.ToDoEntities
                .Where(td => td.User.Name == name)
                .Include(td => td.User)
                .ToListAsync();
        }

        public ToDoEntity UpdateToDo(TdDbContext context, ToDoEntity entity)
        {
            context.ToDoEntities.Update(entity);
            return entity;
        }
    }
}
