using System.ComponentModel.DataAnnotations;

namespace CollegeApp.Models
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required (ErrorMessage = "Student name is required")]
        [StringLength (50)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Student age is required")]
        [Range(18,99, ErrorMessage = "Student must be between 18 and 99")]
        public int Age { get; set; }
        
        public string Email { get; set; }

        [Required(ErrorMessage = "Student address is required")]
        [StringLength (150)]
        public string Address { get; set; }

        //[Required(ErrorMessage = "Student address is required")]
        //[MinLength(8, ErrorMessage = "password length should minimum of 8")]
        //public string Password { get; set; }

        //[Required(ErrorMessage = "Student address is required")]
        //[MinLength(8, ErrorMessage = "password length should minimum of 8")]
        //[Compare(nameof(Password))]
        //public string ConfirmPassword { get; set; }
    }
}
