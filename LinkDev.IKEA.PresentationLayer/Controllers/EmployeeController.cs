﻿using LinkDev.IKEA.BusinesLogicLayer.Models.Employees;
using LinkDev.IKEA.BusinesLogicLayer.Services.Employees;
using LinkDev.IKEA.PresentationLayer.ViewModels.Employees;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PresentationLayer.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly IEmployeeService _employeeService;
        // private readonly IDepartmentService _departmentService;
        private readonly ILogger<EmployeeController> _logger;
        private readonly IWebHostEnvironment _environment;

        public EmployeeController(
            IEmployeeService employeeService,
            ILogger<EmployeeController> logger,
            IWebHostEnvironment environment)
        {
            _employeeService = employeeService;
            // _departmentService = departmentService;
            _logger = logger;
            _environment = environment;
        }


        [HttpGet]
        public IActionResult Index(string search)
        {
            var employee = _employeeService.GetEmployees(search);
            return View(employee);
        }

        #region Create
        [HttpGet]

        // [FromServices] IDepartmentService departmentService
        public IActionResult Create()
        {
            // ViewData["Departments"] = departmentService.GetAllDepartments();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
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
        #endregion

        #region Details
        [HttpGet]
        public IActionResult Details(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }
            var employee = _employeeService.GetEmployeeById(id.Value);

            if (employee is null)
            {
                return NotFound();
            }
            return View(employee);
        }
        #endregion

        #region Update
        [HttpGet]
        public IActionResult Edit(int? id)
        {
            if (id is null)
            {
                return BadRequest();
            }

            var employee = _employeeService.GetEmployeeById(id.Value);
            if (employee is null)
            {
                return NotFound();
            }


            return View(new EmployeeEditViewModel()
            {
                Name = employee.Name,
                Age = employee.Age,
                Address = employee.Address,
                Email = employee.Email,
                Salary = employee.Salary,
                HiringDate = employee.HiringDate,
                IsActive = employee.IsActive,
                PhoneNumber = employee.PhoneNumber,
                Gender = employee.Gender,
                EmployeeType = employee.EmployeeType,
            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, EmployeeEditViewModel employeeVM)
        {
            if (!ModelState.IsValid)
            {
                return View(employeeVM);
            }

            var msg = string.Empty;

            try
            {
                var employeeToUpdate = new UpdatedEmployeeDto()
                {
                    Id = id,
                    Name = employeeVM.Name,
                    Address = employeeVM.Address,
                    Email = employeeVM.Email,
                    Salary = employeeVM.Salary,
                    Age = employeeVM.Age,
                    EmployeeType = employeeVM.EmployeeType,
                    Gender = employeeVM.Gender,
                    HiringDate = employeeVM.HiringDate,
                    IsActive = employeeVM.IsActive,
                    PhoneNumber = employeeVM.PhoneNumber,

                };

                var Update = _employeeService.UpdateEmployee(employeeToUpdate) > 0;
                if (Update)
                {
                    return RedirectToAction("Index");
                }
                msg = "An Error has occured during the updating The Employee";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                msg = _environment.IsDevelopment() ? ex.Message : "An Error has occured during the updating The Employee";
            }

            ModelState.AddModelError(string.Empty, msg);
            return View(employeeVM);
        }
        #endregion

        #region Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var msg = string.Empty;

            try
            {
                var result = _employeeService.DeleteEmployee(id);
                if (result)
                {
                    return RedirectToAction(nameof(Index));
                }
                msg = "an Error occured during deleting the employee";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                msg = _environment.IsDevelopment() ? ex.Message : "An Error has occured during the deleting the department";
            }
            return RedirectToAction("Index");

        } 
        #endregion
    }
}
