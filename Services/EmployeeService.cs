
    using global::SmallBizManager.Data;
    using global::SmallBizManager.Models;
    using SmallBizManager.Data;
    using SmallBizManager.Models;
    using System.Collections.Generic;
    using System.Linq;

    namespace SmallBizManager.Services
    {
    public class EmployeeService : IEmployeeService
    {
        private readonly ApplicationDbContext _context;

        public EmployeeService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create a new employee
        public bool CreateEmployee(Employee employee)
        {
            if (employee == null)
                return false;

            _context.Employees.Add(employee);
            _context.SaveChanges();
            return true;
        }

        // Get all employees
        public List<Employee> GetAllEmployees()
        {
            return _context.Employees.ToList();
        }

        // Get an employee by their ID
        public Employee GetEmployeeById(int employeeId)
        {
            return _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
        }

        public bool UpdateEmployee(Employee employee)
        {
            var existingEmployee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if (existingEmployee == null)
                return false;

            existingEmployee.Name = employee.Name;
            existingEmployee.Role = employee.Role;
            existingEmployee.Email = employee.Email;
            existingEmployee.Phone = employee.Phone;
            existingEmployee.Address = employee.Address;
            existingEmployee.Salary = employee.Salary;



            _context.Employees.Update(existingEmployee);
            _context.SaveChanges();
            return true;
        }

        // Delete an employee by their ID
        public bool DeleteEmployee(int employeeId)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employee == null)
                return false;

            _context.Employees.Remove(employee);
            _context.SaveChanges();
            return true;
        }
    }
}


