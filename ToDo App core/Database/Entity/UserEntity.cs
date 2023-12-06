using Microsoft.EntityFrameworkCore;

namespace ToDo_App_core.Database.Entity
{
    [Index(nameof(Name), IsUnique = true)]
    public class UserEntity
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
    }
}
