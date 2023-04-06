using LoginAuthenticationForm.BAL;
using LoginAuthenticationForm.Model;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Security.Claims;

namespace LoginAuthenticationForm.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {

        private readonly IEmployeeRespository _employeeRepository;
        public EmployeeController(IEmployeeRespository employeeRepository)
        {
            this._employeeRepository = employeeRepository;    
        }


        [Authorize(AuthenticationSchemes=Microsoft.AspNetCore.Authentication.JwtBearer.JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("GetEmployeeList")]
        public IActionResult GetEmployeeList()
        {
            var user_Email = User.FindFirstValue(ClaimTypes.Email);
            var userDetails = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<Employee> employees= _employeeRepository.GetAllEmployees();    
            return Ok(employees);
        }
        [HttpGet("Details")]
        public IActionResult Details(int Id)
        {
            Employee empList = _employeeRepository.GetEmployeeByID(Id);
            if (empList == null)
            {
                return NotFound("The Employee Record Couldn't  be Found");
            }
            return Ok(empList);
        }

        [HttpPost("AddEmployee")]
        public  IActionResult AddEmployee( [FromBody]Employee employee)
        {
            if (employee==null)
            {
                return BadRequest("Employee  is null");
            }
             _employeeRepository.AddNewUser(employee);
            return CreatedAtAction("GetEmployeeList", new { employee = employee});
        }

        [HttpPut("EditEmployee")]
        public IActionResult EditEmployee(int Id , [FromBody]Employee employee) 
        {
            if (employee == null)
            {
                return BadRequest("Employee is null");
            }
                Employee employeeListById = _employeeRepository.GetEmployeeByID(Id);
            if (employeeListById == null) 
            {
                return BadRequest("The Employee Record couldn't be  Found");
            }
               _employeeRepository.UpdateUser(employeeListById, employee);
               return CreatedAtAction("GetEmployeeList", new { EmployeeDetails=employee});
        }

        [HttpDelete("DeleteEmployee")]
         public IActionResult DeleteEmployee(int  Id)
        {
            Employee employeeList = _employeeRepository.GetEmployeeByID(Id);
            if(employeeList == null)
            {
                return BadRequest("Employee is Null ");
            }
            _employeeRepository.DeleteUser(employeeList);
            return CreatedAtAction("GetEmployeeList" ,new {EmployeeRecord= employeeList});
        }
    }
}
