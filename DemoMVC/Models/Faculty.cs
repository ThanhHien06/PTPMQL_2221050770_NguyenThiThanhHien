using System.ComponentModel.DataAnnotations;
namespace Sinhvien.Models;

public class Faculty
{
    [Key]
    [Required(ErrorMessage = "Mã khoa không được để trống")]
    public string FacultyId { get; set; }
    [Required(ErrorMessage = "Tên khoa không được để trống")]
    public string FacultyName { get; set; }

    public ICollection<Student> Students { get; set; }

}

