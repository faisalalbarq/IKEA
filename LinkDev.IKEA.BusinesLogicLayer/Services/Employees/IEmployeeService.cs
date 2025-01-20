using LinkDev.IKEA.BusinesLogicLayer.Models.Employees;

namespace LinkDev.IKEA.BusinesLogicLayer.Services.Employees
{
    public interface IEmployeeService
    {

        Task <IEnumerable<EmployeeDto>> GetEmployeesAsync(string search);// getAll

        Task <EmployeeDetailsDto?> GetEmployeeByIdAsync(int id); //getByid

        Task <int> CreateEmployeeAsync(CreatedEmployeeDto employeeDto);

        Task <int> UpdateEmployeeAsync(UpdatedEmployeeDto employeeDto);

        Task <bool> DeleteEmployeeAsync(int id);
    }
}
