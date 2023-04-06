using System.ComponentModel.DataAnnotations;

namespace LoginAuthenticationForm.Model
{
    //public class Users
    //{
    //    public string UserName { get; set; }
    //    public Guid Id { get; set; }
    //    [Required]
    //    [DataType(DataType.EmailAddress)]
    //    public string EmailId { get; set; }

    //    [DataType(DataType.Password)]
    //    [Required]
    //    public string Password { get; set; }

    //}

    public class UserModel
    {
        [Key]
        public Guid UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Role { get; set; }
    }





}
