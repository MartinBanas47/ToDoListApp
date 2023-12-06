using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using ToDo_App_core.Dto;

namespace ToDo_App_core.Helpers
{
    public static class ExcelFileHelper
    {
        public static void GenerateExcelFile(IEnumerable<ToDoDto> todos, string fileName)
        {
            var directoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Files");
            var path = Path.Combine(directoryPath, fileName);

            using var spreadsheetDocument = SpreadsheetDocument.Create(path, SpreadsheetDocumentType.Workbook);
            var workbookPart = spreadsheetDocument.AddWorkbookPart();
            workbookPart.Workbook = new Workbook();

            var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
            worksheetPart.Worksheet = new Worksheet(new SheetData());

            var sheets = spreadsheetDocument.WorkbookPart?.Workbook.AppendChild(new Sheets());
            var sheet = new Sheet { Id = spreadsheetDocument.WorkbookPart?.GetIdOfPart(worksheetPart), SheetId = 1, Name = "To-Do Items" };
            sheets?.Append(sheet);

            var sheetData = worksheetPart.Worksheet.GetFirstChild<SheetData>();

            var headerRow = new Row();
            headerRow.Append(
                CreateCell("ID", CellValues.String),
                CreateCell("Text", CellValues.String),
                CreateCell("User Name", CellValues.String),
                CreateCell("Due Date", CellValues.String)
            );
            headerRow.ThickBot = true;

            sheetData?.AppendChild(headerRow);

            SetColumnWidth(worksheetPart, 1, 12);
            SetColumnWidth(worksheetPart, 2, 30);
            SetColumnWidth(worksheetPart, 3, 20);
            SetColumnWidth(worksheetPart, 4, 15);

            for (var i = 0; i < todos.Count(); i++)
            {
                var todo = todos.ElementAt(i);

                var dataRow = new Row();
                dataRow.Append(
                    CreateCell(todo.Id.ToString(), CellValues.Number),
                    CreateCell(todo.Text, CellValues.String),
                    CreateCell(todo.UserName, CellValues.String),
                    CreateCell(todo.Date?.ToString("yyyy-MM-dd") ?? "", CellValues.String)
                );
                sheetData?.AppendChild(dataRow);
            }

            worksheetPart.Worksheet.Save();
        }

        private static Cell CreateCell(string value, CellValues dataType)
        {
            var cell = new Cell(new CellValue(value)) { DataType = new EnumValue<CellValues>(dataType) };
            return cell;
        }

        private static void SetColumnWidth(WorksheetPart worksheetPart, uint columnIndex, double width)
        {
            var columns = worksheetPart.Worksheet.GetFirstChild<Columns>();
            if (columns == null)
            {
                columns = new Columns();
                worksheetPart.Worksheet.InsertBefore(columns, worksheetPart.Worksheet.GetFirstChild<SheetData>());
            }

            var column = new Column { Min = columnIndex, Max = columnIndex, Width = width, CustomWidth = true };
            columns.Append(column);
        }
    }
}
