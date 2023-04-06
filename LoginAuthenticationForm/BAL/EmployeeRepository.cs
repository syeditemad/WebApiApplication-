using LoginAuthenticationForm.Model;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace LoginAuthenticationForm.BAL
{
    public class EmployeeRepository : IEmployeeRespository
    {
        private readonly EmployeContext _Context;

        public EmployeeRepository(EmployeContext context)
        {
            this._Context = context;
        }
        public IEnumerable<Employee> GetAllEmployees()
        {
            return _Context.employees.ToList();
        }

        /// <summary>
        /// Get Details By using Id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Employee GetEmployeeByID(int id)
        {
               var employeeList = _Context.employees.FirstOrDefault(x => x.EmployeeID == id);
               return (employeeList);
           
        }

        /// <summary>
        /// AddNew  New Record
        /// </summary>
        /// <param name="employee"></param>

        public void AddNewUser(Employee employee)
        {
            _Context.employees.Add(employee);
            _Context.SaveChanges();
        }


        /// <summary>
        /// Update the Employee Record By Using Id 
        /// </summary>
        /// <param name="employeeListById"></param>
        /// <param name="employee"></param>
        public void UpdateUser(Employee employeeListById, Employee employee)
        {
           // employeeListById.EmployeeID = employee.EmployeeID;
            employeeListById.EmployeeName = employee.EmployeeName;
            employeeListById.Email = employee.Email;
            employeeListById.salary = employee.salary;
            employeeListById.DOB = employee.DOB;
            _Context.SaveChanges(true);

        }

        /// <summary>
        /// Delete Employee Record BY Id  
        /// </summary>
        /// <param name="employee"></param>
        public void DeleteUser(Employee employee)
        {
            _Context.employees.Remove(employee);
            _Context.SaveChanges();
        }

        /// <summary>
        /// User Registration for login 
        /// </summary>
        /// <param name="userModel"></param>
       

        public async void UserRegistration(UserModel userModel)
        {
            if(userModel == null)
            {
                throw new ArgumentNullException(nameof(userModel));
            }

             await _Context.AddAsync(userModel);
                   _Context.SaveChangesAsync();
        }
    }
}
