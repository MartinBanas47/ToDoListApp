using ToDo_App_core.Dto;

namespace ToDo_App_core.Services.ToDo
{
    public interface IToDoService
    {
        Task<bool> CreateToDo(string userName, string text, DateTime? dateTime = null);
        Task<bool> DeleteToDo(int id);
        Task<bool> DeleteOldToDos();
        Task<bool> UpdateToDo(ToDoDto toDoDto);
        Task<IEnumerable<ToDoDto>> GetToDos();
        Task<IEnumerable<ToDoDto>> GetToDosForPerson(string userName);
    }
}
