using ToDo_App_core.Database.Entity;

namespace ToDo_App_core.Database
{
    public static class SeedData
    {
        public static void Initialize()
        {
            using var context = new TdDbContext();

            if (context.Users.Any())
            {
                return;
            }

            context.Users.AddRange(
                new UserEntity
                {
                    Name = "Alice"
                },
                new UserEntity
                {
                    Name = "Bob"
                });

            context.SaveChanges();


            if (context.ToDoEntities.Any())
            {
                return;
            }

            context.ToDoEntities.AddRange(
                new ToDoEntity
                {
                    Text = "Do the dishes",
                    Date = DateTime.Now,
                    UserId = context.Users.Single(u => u.Name == "Alice").Id
                },
                new ToDoEntity
                {
                    Text = "Walk the dog",
                    Date = DateTime.Now.AddDays(1),
                    UserId = context.Users.Single(u => u.Name == "Alice").Id
                },
                new ToDoEntity
                {
                    Text = "Buy groceries",
                    Date = DateTime.Now.AddDays(2),
                    UserId = context.Users.Single(u => u.Name == "Bob").Id
                });

            context.SaveChanges();
        }
    }
}
