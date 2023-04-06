using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Runtime.CompilerServices;

namespace LoginAuthenticationForm.Model
{
    public class Employee
    {
        [Key]
        public int EmployeeID { get; set; }
        [Required(ErrorMessage ="EployeeName is Mandatory")]
        public string? EmployeeName { get; set; }
        [Required(ErrorMessage ="Email is mandatory")]
        public string? Email { get; set; }
        public int salary { get; set; }
        public DateTime DOB { get; set; }

    }
}
