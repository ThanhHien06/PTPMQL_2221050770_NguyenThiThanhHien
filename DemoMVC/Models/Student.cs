using System.ComponentModel.DataAnnotations;
namespace Sinhvien.Models
{
    public class Student
    {
        [Key]
        public string StudentCode { get; set; } =default!;
        public string? FullName { get; set; }
    }
}
