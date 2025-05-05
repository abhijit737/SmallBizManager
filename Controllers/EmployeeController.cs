using Microsoft.AspNetCore.Mvc;
using global::SmallBizManager.Data;
using global::SmallBizManager.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SmallBizManager.Data;
using SmallBizManager.Models;
using SmallBizManager.Services;
namespace SmallBizManager.Controllers
{

    [Authorize(Policy = "AdminOnly")]

    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;

        public EmployeeController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public IActionResult Index() => View(_employeeService.GetAllEmployees());

        public IActionResult Create()
        {
            return View(new Employee()); 
        }
        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.CreateEmployee(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public IActionResult Edit(int id) => View(_employeeService.GetEmployeeById(id));

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (ModelState.IsValid)
            {
                _employeeService.UpdateEmployee(employee);
                return RedirectToAction("Index");
            }

            return View(employee);
        }

        public IActionResult Delete(int id)
        {
            _employeeService.DeleteEmployee(id);
            return RedirectToAction("Index");
        }
    }
}

