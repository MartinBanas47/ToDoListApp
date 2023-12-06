using System.ComponentModel.DataAnnotations.Schema;

namespace ToDo_App_core.Database.Entity
{
    public class ToDoEntity
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public DateTime? Date { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public virtual UserEntity User { get; set; }
    }
}
