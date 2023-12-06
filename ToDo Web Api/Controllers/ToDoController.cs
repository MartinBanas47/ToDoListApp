using Microsoft.AspNetCore.Mvc;
using ToDo_App_core.Dto;
using ToDo_App_core.Services.ToDo;

namespace ToDo_Web_Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ToDoController : ControllerBase
    {
        private readonly IToDoService _toDoService;

        public ToDoController(IToDoService toDoService)
        {
            _toDoService = toDoService;
        }

        [HttpGet]
        public async Task<IEnumerable<ToDoDto>> GetToDos()
        {
            return await _toDoService.GetToDos();
        }

        [HttpDelete("{id}")]
        public async Task<bool> DeleteToDos(int id)
        {
            return await _toDoService.DeleteToDo(id);
        }

        [HttpDelete]
        public async Task<bool> DeleteOldToDos()
        {
            return await _toDoService.DeleteOldToDos();
        }

        [HttpGet("{username}")]
        public async Task<IEnumerable<ToDoDto>> GetToDos(string username)
        {
            return await _toDoService.GetToDosForPerson(username);

        }

        [HttpPost]
        public async Task<bool> CreateToDo(string username, string text, DateTime? dateTime)
        {
            return await _toDoService.CreateToDo(username, text, dateTime);
        }

        [HttpPut]
        public async Task<bool> UpdateToDo(ToDoDto toDoDto)
        {
            return await _toDoService.UpdateToDo(toDoDto);
        }
    }
}
