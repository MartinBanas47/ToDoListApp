using Autofac;
using ToDo_App_core.Database;
using ToDo_App_core.Repository.ToDo;
using ToDo_App_core.Repository.User;
using ToDo_App_core.Services.ToDo;
using ToDo_App_core.Services.User;
using ToDo_list_app;
using ToDo_list_app.Helpers;

var builder = new ContainerBuilder();

builder.RegisterType<UserRepository>().As<IUserRepository>();
builder.RegisterType<ToDoRepository>().As<IToDoRepository>();
builder.RegisterType<UserService>().As<IUserService>();
builder.RegisterType<ToDoService>().As<IToDoService>();
builder.RegisterType<CommandService>();
var container = builder.Build();
SeedData.Initialize();
using var scope = container.BeginLifetimeScope();

var consoleService = scope.Resolve<CommandService>();


while (true)
{
    try
    {
        await consoleService.ReadCommandAsync();
    }
    catch (Exception e)
    {
        ConsoleHelper.WriteLine("Error occured: " + e.Message);
    }


}
