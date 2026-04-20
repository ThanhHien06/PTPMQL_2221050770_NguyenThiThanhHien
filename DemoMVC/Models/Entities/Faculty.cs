using System.ComponentModel.DataAnnotations;
using DemoMVC.Models.Entities;
namespace DemoMVC.Models.Entities;

public class Faculty
{
    [Key]
    [Required(ErrorMessage = "Mã khoa không được để trống")]
    public string FacultyId { get; set; }
    [Required(ErrorMessage = "Tên khoa không được để trống")]
    public string FacultyName { get; set; }

    public ICollection<Student> Students { get; set; }

}

