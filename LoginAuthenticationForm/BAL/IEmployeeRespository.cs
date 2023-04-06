using LoginAuthenticationForm.Model;

namespace LoginAuthenticationForm.BAL
{
    public interface IEmployeeRespository
    {
        IEnumerable<Employee> GetAllEmployees();
        Employee GetEmployeeByID(int id);
        void AddNewUser(Employee employee);
        void UpdateUser(Employee employeeListById, Employee employee);
        void DeleteUser(Employee employee);

        void UserRegistration(UserModel userModel);
    }
}
