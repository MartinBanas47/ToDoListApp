namespace ToDo_App_core.Dto
{
    public class ToDoDto
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public DateTime? Date { get; set; }
    }
}
