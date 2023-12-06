using Microsoft.EntityFrameworkCore;
using ToDo_App_core.Database.Entity;

namespace ToDo_App_core.Database
{
    public class TdDbContext : DbContext
    {
        public DbSet<UserEntity> Users { get; set; }
        public DbSet<ToDoEntity> ToDoEntities { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseInMemoryDatabase("FindTheTreasure");
        }
    }
}
