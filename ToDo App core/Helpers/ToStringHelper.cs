using System.Text;
using ToDo_App_core.Dto;

namespace ToDo_App_core.Helpers
{
    public static class ToStringHelper
    {
        public static string NamesListToString(IEnumerable<string> names)
        {
            var result = "Names\n";
            result += "---------------\n";
            result += string.Join("\n", names);
            return result;
        }

        public static string ToDosListToString(IEnumerable<ToDoDto> toDoDtos)
        {
            var columnWidths = new Dictionary<string, int>()
            {
                { "ID", 5 },
                { "Text", 30 },
                { "User Name", 15 },
                { "Due Date", 10 }
            };

            var tableBuilder = new StringBuilder();

            foreach (var columnHeader in columnWidths.Keys)
            {
                tableBuilder.Append($"| {columnHeader.PadRight(columnWidths[columnHeader])} ");
            }
            tableBuilder.AppendLine("|");

            foreach (var columnHeader in columnWidths.Keys)
            {
                tableBuilder.Append($"| {new string('-', columnWidths[columnHeader])} ");
            }
            tableBuilder.AppendLine("|");

            foreach (var todo in toDoDtos)
            {
                tableBuilder.AppendLine($"| {todo.Id.ToString().PadRight(columnWidths["ID"])} | {todo.Text.PadRight(columnWidths["Text"])} | {todo.UserName.PadRight(columnWidths["User Name"])} | {todo.Date?.ToString("yyyy-MM-dd").PadRight(columnWidths["Due Date"])} |");
            }

            return tableBuilder.ToString();
        }
    }
}
