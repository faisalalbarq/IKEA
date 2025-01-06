using LinkDev.IKEA.BusinesLogicLayer.Models.Employees;
using LinkDev.IKEA.DataAccessLayer.Models.Employees;
using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Employees;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BusinesLogicLayer.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public IEnumerable<EmployeeDto> GetAllEmployees()
        {
            // فهذا الكود وين بتنفذ ؟IEnumerable بما اني برجع 
            // ال وير هاي بتتنفذ تبعت الاي كوارابيل مش الاي نيورابيل لاني بنفذها من خلال اوبجكت من كلاس يامبليمينت الاي كوارابيل
            //فرح يريجع اينيورابيل لانه الايكورابيل هو ابن الاينيورابيل هذا يعني بان كود الريتيرن لو خزنته بفاريابيل مارح يتنفذ لانه كويري
            // بس لمه حكيت بدي اعمله ريتيرن كاينيورابيل يعني بدي الدنيا تتزبط وترجعلنا عشان نيوميرات عليها 
            // فالكويري رح يتنفذ بال سيكوال        
            return _employeeRepository
                .GetAllAsIQueryable()
                .Where(E => !E.IsDeleted)
                .Select(employee => new EmployeeDto()
            {
                Id = employee.Id,
                Name = employee.Name,
                Salary = employee.Salary,
                Age = employee.Age,
                IsActive = employee.IsActive,
                Email = employee.Email,
                Gender = employee.Gender.ToString(),
                EmployeeType = employee.EmployeeType.ToString()
            }).ToList();
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee is { })
                return new EmployeeDetailsDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Address = employee.Address,
                    Salary = employee.Salary,
                    Age = employee.Age,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    PhoneNumber = employee.PhoneNumber,
                    HiringDate = employee.HiringDate,
                    Gender = employee.Gender,
                    EmployeeType = employee.EmployeeType,

                };
            return null;
        }


        public int CreateEmployee(CreatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Name = employeeDto.Name,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                Age = employeeDto.Age,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                LastModifiedBy = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            return _employeeRepository.Add(employee);
        }

        public int UpdateEmployee(UpdatedEmployeeDto employeeDto)
        {
            var employee = new Employee()
            {
                Id = employeeDto.Id,
                Name = employeeDto.Name,
                Address = employeeDto.Address,
                Salary = employeeDto.Salary,
                Age = employeeDto.Age,
                IsActive = employeeDto.IsActive,
                Email = employeeDto.Email,
                PhoneNumber = employeeDto.PhoneNumber,
                HiringDate = employeeDto.HiringDate,
                Gender = employeeDto.Gender,
                EmployeeType = employeeDto.EmployeeType,
                LastModifiedBy = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            return _employeeRepository.Update(employee);
        }

        public bool DeleteEmployee(int id)
        {
            var employee = _employeeRepository.Get(id);
            if (employee is { })
            {
                return _employeeRepository.Delete(employee) > 0;
            }
            return false;
        }




    }
}
