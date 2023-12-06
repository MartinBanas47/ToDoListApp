using ToDo_App_core.Database;
using ToDo_App_core.Database.Entity;
using ToDo_App_core.Dto;
using ToDo_App_core.Repository.ToDo;
using ToDo_App_core.Repository.User;

namespace ToDo_App_core.Services.ToDo
{
    public class ToDoService : IToDoService
    {
        private readonly IToDoRepository _toDoRepository;
        private readonly IUserRepository _userRepository;

        public ToDoService(IToDoRepository toDoRepository, IUserRepository userRepository)
        {
            _toDoRepository = toDoRepository;
            _userRepository = userRepository;
        }
        public async Task<bool> CreateToDo(string userName, string text, DateTime? dateTime = null)
        {
            using var dbContext = new TdDbContext();
            var user = await _userRepository.GetUserAsync(dbContext, userName);
            if (user == null)
            {
                return false;
            }
            await _toDoRepository.CreateToDoAsync(dbContext, text, user.Id, dateTime);
            await dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteOldToDos()
        {
            using var dbContext = new TdDbContext();
            var result = await _toDoRepository.DeleteOldToDosAsync(dbContext);
            await dbContext.SaveChangesAsync();
            return result;

        }

        public async Task<bool> DeleteToDo(int id)
        {
            using var dbContext = new TdDbContext();
            var result = await _toDoRepository.DeleteToDoAsync(dbContext, id);
            await dbContext.SaveChangesAsync();
            return result;
        }

        public async Task<IEnumerable<ToDoDto>> GetToDos()
        {
            using var dbContext = new TdDbContext();
            return (await _toDoRepository.GetToDosAsync(dbContext))
                .Select(td => new ToDoDto
                {
                    Id = td.Id,
                    Text = td.Text,
                    UserName = td.User.Name,
                    Date = td.Date
                })
                .OrderBy(td => td.UserName);
        }

        public async Task<IEnumerable<ToDoDto>> GetToDosForPerson(string userName)
        {
            using var dbContext = new TdDbContext();
            return (await _toDoRepository.GetToDosForPersonAsync(dbContext, userName))
                .Select(td => new ToDoDto
                {
                    Id = td.Id,
                    Text = td.Text,
                    UserName = td.User.Name,
                    Date = td.Date
                });
        }

        public async Task<bool> UpdateToDo(ToDoDto toDoDto)
        {
            using var dbContext = new TdDbContext();
            var toDo = await _toDoRepository.GetToDoAsync(dbContext, toDoDto.Id);
            var user = await _userRepository.GetUserAsync(dbContext, toDoDto.UserName);
            if (toDo == null || user == null)
                return false;
            FillToDoEntityForUpdate(toDoDto, toDo, user);
            dbContext.ToDoEntities.Update(toDo);
            await dbContext.SaveChangesAsync();
            return true;
        }

        private static void FillToDoEntityForUpdate(ToDoDto toDoDto, ToDoEntity toDo, UserEntity user)
        {
            toDo.UserId = user.Id;
            toDo.Text = toDoDto.Text;
            toDo.Date = toDoDto.Date;
        }
    }
}
