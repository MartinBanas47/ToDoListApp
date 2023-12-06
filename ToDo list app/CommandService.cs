using ToDo_App_core.Helpers;
using ToDo_App_core.Services.ToDo;
using ToDo_App_core.Services.User;
using ToDo_list_app.Helpers;

namespace ToDo_list_app
{

    public class CommandService
    {
        private const string ErrorMessage = "Error occured. Make sure you have inserted correct data.";
        private readonly IToDoService _toDoService;
        private readonly IUserService _userService;

        public event EventHandler? ErrorEventHandler;

        public CommandService(IToDoService toDoService, IUserService userService)
        {
            _toDoService = toDoService;
            _userService = userService;
            ConsoleHelper.WriteLine("Welcome to ToDo app");
            ConsoleHelper.WriteLine("You can use command \"help\" if needed");
        }



        public async Task ReadCommandAsync()
        {
            try
            {
                string? line = ConsoleHelper.ReadLine();
                if (string.IsNullOrEmpty(line))
                    return;
                string[] commandWithArgs = line.Trim().Split(' ');
                switch (commandWithArgs[0])
                {
                    case "add-user":
                        await AddUserAsync(commandWithArgs);
                        break;
                    case "update-user":
                        await UpdateUserAsync(commandWithArgs);
                        break;
                    case "delete-user":
                        await RemoveUserAsync(commandWithArgs);
                        break;
                    case "get-users":
                        await GetUserAsync(commandWithArgs);
                        break;
                    case "add-todo":
                        await CreateToDoAsync(commandWithArgs);
                        break;
                    case "update-todo":
                        await UpdateToDoAsync(commandWithArgs);
                        break;
                    case "delete-todo":
                        await DeleteToDoAsync(commandWithArgs);
                        break;
                    case "delete-old-todos":
                        await DeleteOldToDosAsync(commandWithArgs);
                        break;
                    case "get-todos":
                        await GetTodosAsync();
                        break;
                    case "get-todos-person":
                        await GetTodosForPerson(commandWithArgs);
                        break;
                    case "get-todos-file":
                        await GenerateToDosTextFile(commandWithArgs);
                        break;
                    case "get-todos-excel":
                        await GenerateToDosExcelFile(commandWithArgs);
                        break;
                    case "help":
                        HelpCommand();
                        break;
                    default:
                        throw new ArgumentException("Invalid command");
                }
            }
            catch (Exception e)
            {
                ConsoleHelper.WriteLine("Error occured: " + e.Message);
            }
        }

        private async Task GenerateToDosExcelFile(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 0);
            var todos = await _toDoService.GetToDos();
            ExcelFileHelper.GenerateExcelFile(todos, $"ToDos_{DateTime.Now.Date:dd.MM.yy}.xlsx");
        }

        private async Task GenerateToDosTextFile(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 0);
            var todos = await _toDoService.GetToDos();
            TextFileHelper.CreateTextFile($"ToDos_{DateTime.Now.Date:dd.MM.yy}.txt", ToStringHelper.ToDosListToString(todos));
        }

        private async Task GetTodosForPerson(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 1);
            await _toDoService.GetToDosForPerson(commandWithArgs[1]);
        }

        private async Task DeleteOldToDosAsync(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 0);
            var result = await _toDoService.DeleteOldToDos();
            CheckError(result);
        }

        private async Task DeleteToDoAsync(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 1);
            var result = await _toDoService.DeleteToDo(ValidateIntArgument(commandWithArgs[0], "Id"));
            CheckError(result);
        }

        private async Task UpdateToDoAsync(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 1);
            var result = await _toDoService.CreateToDo(commandWithArgs[1], commandWithArgs[2], DateTime.Parse(commandWithArgs[3]));
            CheckError(result);
        }

        private async Task CreateToDoAsync(string[] commandWithArgs)
        {
            var result = false;
            if (commandWithArgs.Count() == 4)
            {
                CheckNumberOfArgs(commandWithArgs, 3);
                result = await _toDoService.CreateToDo(commandWithArgs[1], commandWithArgs[2], DateTime.Parse(commandWithArgs[3]));
            }
            else
            {
                CheckNumberOfArgs(commandWithArgs, 2);
                result = await _toDoService.CreateToDo(commandWithArgs[1], commandWithArgs[2]);
            }
            CheckError(result);
        }

        private static void HelpCommand()
        {
            ConsoleHelper.WriteLine("Commands: ");
            ConsoleHelper.WriteLine("\tadd-user <name>: Add a new user with the given name.");
            ConsoleHelper.WriteLine("\tupdate-user <old_name> <new_name>: Update an existing user's name.");
            ConsoleHelper.WriteLine("\tdelete-user <name>: Delete an existing user.");
            ConsoleHelper.WriteLine("\tget-users: Get a list of all existing user names.");
            ConsoleHelper.WriteLine("\tadd-todo <user_name> <text> <due_date (optional)>: Add a new to-do item for the specified user with the given text and optional due date.");
            ConsoleHelper.WriteLine("\tupdate-todo <id> <user_name> <text> <due_date (optional)>: Update an existing to-do item with the specified ID.");
            ConsoleHelper.WriteLine("\tdelete-todo <id>: Delete an existing to-do item with the specified ID.");
            ConsoleHelper.WriteLine("\tdelete-old-todos: Delete all to-do items that are past their due dates.");
            ConsoleHelper.WriteLine("\tget-todos: Get a list of all to-do items.");
            ConsoleHelper.WriteLine("\tget-todos-user <user_name>: Get a list of all to-do items for the specified user.");
            ConsoleHelper.WriteLine("\tget-todos-file: Get a list of all to-do items and export it into file.");
            ConsoleHelper.WriteLine("\thelp: Display this list of commands.");

        }
        private async Task AddUserAsync(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 1);
            var result = await _userService.CreateUserAsync(commandWithArgs[1]);
            CheckError(result);
        }

        private async Task UpdateUserAsync(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 2);
            var result = await _userService.UpdateUserAsync(commandWithArgs[1], commandWithArgs[2]);
            CheckError(result);
        }

        private async Task RemoveUserAsync(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 1);
            var result = await _userService.RemoveUserAsync(commandWithArgs[1]);
            CheckError(result);
        }

        private async Task GetUserAsync(string[] commandWithArgs)
        {
            CheckNumberOfArgs(commandWithArgs, 0);
            ConsoleHelper.WriteLine(ToStringHelper.NamesListToString(await _userService.GetUserNamesAsync()));
        }

        private async Task GetTodosAsync()
        {
            ConsoleHelper.WriteLine(ToStringHelper.ToDosListToString(await _toDoService.GetToDos()));
        }

        private static int ValidateIntArgument(string argument, string argumentName)
        {
            if (!int.TryParse(argument, out int result))
                throw new ArgumentException($"Argument {argumentName} in should be number");
            return result;
        }
        private static void CheckNumberOfArgs(string[] commandWithArgs, int numberOfArgs)
        {
            if (commandWithArgs.Count(x => !string.IsNullOrEmpty(x.Trim())) != numberOfArgs + 1)
                throw new ArgumentException("Invalid number of arguments for provided command");
        }

        private static void CheckError(bool success)
        {
            if (!success)
                ConsoleHelper.WriteLine(ErrorMessage);
        }

    }
}