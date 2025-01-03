﻿using LinkDev.IKEA.BusinesLogicLayer.Models.Employees;

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
