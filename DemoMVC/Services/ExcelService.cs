using OfficeOpenXml;
using System.Linq;
using DemoMVC.Models.Entities;

namespace DemoMVC.Services
{
    public class ExcelService
    {
        public List<Student> ReadStudentsFromExcel(string filePath)
        {
            var students = new List<Student>();

            using (var package = new ExcelPackage(new FileInfo(filePath)))
            {
                var worksheet = package.Workbook.Worksheets.FirstOrDefault();

                if (worksheet == null)
                    throw new Exception("Không có sheet trong file Excel");

                int rowCount = worksheet.Dimension.Rows;

                for (int row = 2; row <= rowCount; row++)
                {
                    students.Add(new Student
                    {
                        StudentCode = worksheet.Cells[row, 1].Text,
                        FullName = worksheet.Cells[row, 2].Text,
                        Age = int.Parse(worksheet.Cells[row, 3].Text),
                        Email = worksheet.Cells[row, 4].Text,
                        FacultyID = worksheet.Cells[row, 5].Text
                    });
                }
            }

            return students;
        }
    }
}