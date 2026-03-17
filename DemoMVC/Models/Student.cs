using System.ComponentModel.DataAnnotations;
namespace Sinhvien.Models
{
    public class Student
    {
        [Key]
        
        [Required(ErrorMessage = "Mã sinh viên không được để trống")]
        public string StudentCode { get; set; } 

        [Required(ErrorMessage = "Tên không được để trống")]
        [StringLength(30)]
        public string FullName { get; set; } 
        
        [Required(ErrorMessage = "Tuổi không được để trống")]
        [Range(18,60, ErrorMessage = "Tuổi phải từ 18 đến 60")]
        public int Age { get; set;}

        [EmailAddress(ErrorMessage = "Email không hợp lệ")]
        public string Email { get; set; }
    }
}
