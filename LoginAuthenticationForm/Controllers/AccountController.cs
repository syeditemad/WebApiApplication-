using LoginAuthenticationForm.DAL;
using LoginAuthenticationForm.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using LoginAuthenticationForm.BAL;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace LoginAuthenticationForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class AccountController :ControllerBase
    {
        private readonly IEmployeeRespository _employeeRespository;
         private readonly IConfiguration _config;
        public AccountController(IConfiguration Config, IEmployeeRespository employeeRespository)
        {
            this._config= Config;
            this._employeeRespository= employeeRespository;
        }

        public static List<UserModel> logins = new(){
            new UserModel() {
                     //  Id = Guid.NewGuid(),
                        Email = "itemad.hyder1997@gmail.com",
                        Username = "Admin",
                        Password = "Admin",
                        Role="Admin",
                },
                new UserModel() {
                  //  Id = Guid.NewGuid(),
                        Email = "admin1997@gmail.com",
                        Username = "User1   ",
                        Password = "Admin",
                        Role="Admin",
                }
        };

        [AllowAnonymous]
        [HttpPost("Login")]
        public ActionResult Login([FromBody] UserLogin userLogin)
            {
            var user = Authenticate(userLogin);
            if (user != null)
            {
                var token = GenerateToken(user);
                return Ok(token);
            }
            return NotFound("user not found");
        }


        [AllowAnonymous]
        [HttpPost("Registration")]
         
        public async Task<IActionResult> UserRegistration(UserModel userModel)
        {
            if(userModel == null)
            {
                return BadRequest("User Details is Empty");
            }
            _employeeRespository.UserRegistration(userModel);
            return Ok(userModel);
        }

        [HttpPost("Logout")]
        [AllowAnonymous]
        public IActionResult Logout()
        {

               HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
               var StoreCokies = Request.Cookies.Keys;
                foreach (var key in StoreCokies)
                {
                    StoreCokies.Remove(key);

                }
             
             return Ok( new { Meesage= "Logout Successfully."});
        }

        private string GenerateToken(UserModel user)
          {
            // var User_name = HttpContext.User.Identities.FirstOrDefault(x=>x.Name==user.Username).Name;        
            //var user_Name = ClaimTypes.NameIdentifier == user.Username;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier,user.Username),
                new Claim(ClaimTypes.Email,user.Email),
                new Claim(ClaimTypes.Role,user.Role)
            };
            var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                _config["Jwt:Audience"],
                claims,
                expires: DateTime.Now.AddMinutes(15),
                signingCredentials: credentials);
                return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private UserModel Authenticate(UserLogin userLogin)
        {
            var currentUser = logins.FirstOrDefault(x => x.Username.ToLower() ==
                userLogin.Username.ToLower() && x.Password == userLogin.Password && x.Email.ToLower()==userLogin.Email.ToLower());
            if (currentUser != null)
            {
                return currentUser;
            }
            return null;
        }
    }
}
