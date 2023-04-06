using LoginAuthenticationForm.Model;
using Microsoft.EntityFrameworkCore;
namespace LoginAuthenticationForm.BAL
{
    public class EmployeContext : DbContext 
    {

        public EmployeContext(DbContextOptions options) :base(options)
        {
        }
        public DbSet<Employee> employees { get; set; }
        public DbSet<UserModel> UserRegistration { get; set; }

    }
}
