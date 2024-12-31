using LinkDev.IKEA.BusinesLogicLayer.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BusinesLogicLayer.Services.Employees
{
    public interface IEmployeeService
    {

        IEnumerable<EmployeeDto> GetAllEmployees();// getAll

        EmployeeDetailsDto? GetEmployeeById(int id); //getByid

        int CreateEmployee(CreatedEmployeeDto employeeDto);

        int UpdateEmployee(UpdatedEmployeeDto employeeDto);

        bool DeleteEmployee(int id);
    }
}
