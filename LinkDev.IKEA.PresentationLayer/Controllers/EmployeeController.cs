using LinkDev.IKEA.BusinesLogicLayer.Models.Departments;
using LinkDev.IKEA.BusinesLogicLayer.Models.Employees;
using LinkDev.IKEA.BusinesLogicLayer.Services.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(IEmployeeService employeeService, ILogger<EmployeeController> logger, IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            _logger = logger;
            _environment = environment;
        }


        [HttpGet]
        public IActionResult Index()
        {
            var employee = _employeeService.GetAllEmployees();
            return View(employee);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        public IActionResult Create(CreatedEmployeeDto employee)
        {
            if (!ModelState.IsValid)
                return View(employee);

            string msg = string.Empty;
            try
            {
                var result = _employeeService.CreateEmployee(employee);// number of records

                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    msg = "Employee is Not Created";
                    ModelState.AddModelError(string.Empty, msg);
                    return View(employee);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                if (_environment.IsDevelopment())
                {
                    msg = ex.Message;
                    return View(employee);
                }
                else
                {
                    msg = "Employee is Not Created";
                    return View("Error", msg);
                }
            }
        }

    }
}
