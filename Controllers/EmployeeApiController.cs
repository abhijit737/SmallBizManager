using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallBizManager.Models;
using SmallBizManager.Services;

namespace SmallBizManager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    
    public class EmployeeApiController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeApiController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet]
        //[Authorize(Policy = "AdminOnly")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Policy = "AdminOnly")]

        public IActionResult GetAll()
        {
            
            


                var employees = _employeeService.GetAllEmployees();
                return Ok(employees);
            
            
        }

        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            return Ok(employee);
        }

        
        [HttpPost]
        public IActionResult Create([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _employeeService.CreateEmployee(employee);
            return CreatedAtAction(nameof(Get), new { id = employee.EmployeeId }, employee);
        }

        
        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var existingEmployee = _employeeService.GetEmployeeById(id);
            if (existingEmployee == null)
                return NotFound();

            //existingEmployee.Name = employee.Name;
            //existingEmployee.Position = employee.Position;
            //existingEmployee.Salary = employee.Salary;

            _employeeService.UpdateEmployee(employee);
            return NoContent();
        }

         [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var employee = _employeeService.GetEmployeeById(id);
            if (employee == null)
                return NotFound();

            _employeeService.DeleteEmployee(id);
            return NoContent();
        }
    }
}
