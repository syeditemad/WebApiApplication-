using System.ComponentModel.DataAnnotations;

namespace LoginAuthenticationForm.Model
{
    //public class UserLogin
    //{
    //    [Required]
    //    public string  UserName { get; set; }
    //    [Required]
    //    public string  Password { get; set; }
    //    public UserLogin() {}

    //}

    public class UserLogin
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
    }

}
