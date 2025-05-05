using SmallBizManager.Models;

namespace SmallBizManager.Services
{
    public interface IEmployeeService
    {
        bool CreateEmployee(Employee employee);
        List<Employee> GetAllEmployees();
        Employee GetEmployeeById(int employeeId);
        bool UpdateEmployee(Employee employee);
        bool DeleteEmployee(int employeeId);
    }
}
