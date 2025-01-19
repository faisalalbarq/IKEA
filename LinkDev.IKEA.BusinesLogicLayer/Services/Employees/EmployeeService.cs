using LinkDev.IKEA.BusinesLogicLayer.Common.Services.Attachments;
using LinkDev.IKEA.BusinesLogicLayer.Models.Employees;
using LinkDev.IKEA.DataAccessLayer.Models;
using LinkDev.IKEA.DataAccessLayer.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.BusinesLogicLayer.Services.Employees
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IAttachmentService _attachmentService;

        public EmployeeService(IUnitOfWork unitOfWork, IAttachmentService AttachmentService)
        {
            _unitOfWork = unitOfWork;
            _attachmentService = AttachmentService;
        }


        public IEnumerable<EmployeeDto> GetEmployees(string search)
        {
            // فهذا الكود وين بتنفذ ؟IEnumerable بما اني برجع 
            // ال وير هاي بتتنفذ تبعت الاي كوارابيل مش الاي نيورابيل لاني بنفذها من خلال اوبجكت من كلاس يامبليمينت الاي كوارابيل
            //فرح يريجع اينيورابيل لانه الايكورابيل هو ابن الاينيورابيل هذا يعني بان كود الريتيرن لو خزنته بفاريابيل مارح يتنفذ لانه كويري
            // بس لمه حكيت بدي اعمله ريتيرن كاينيورابيل يعني بدي الدنيا تتزبط وترجعلنا عشان نيوميرات عليها 
            // فالكويري رح يتنفذ بال سيكوال        
            return _unitOfWork.employeeRepository
                .GetAllAsIQueryable()
                .Where(E => !E.IsDeleted && (string.IsNullOrEmpty(search) || E.Name.ToLower().Contains(search.ToLower())))
                // The contains not ignor the caseSensetive
                .Include(E => E.Department)
                .Select(employee => new EmployeeDto()
                {
                    Id = employee.Id,
                    Name = employee.Name,
                    Salary = employee.Salary,
                    Age = employee.Age,
                    IsActive = employee.IsActive,
                    Email = employee.Email,
                    Gender = employee.Gender.ToString(),
                    EmployeeType = employee.EmployeeType.ToString(),
                    Department = employee.Department != null ? employee.Department.Name : "No Department"


                }).ToList();
        }

        public EmployeeDetailsDto? GetEmployeeById(int id)
        {
            var employee = _unitOfWork.employeeRepository.Get(id);
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
                    Department = employee.Department?.Name ?? "Unknown",
                    Image = employee.Image
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
                DepartmentId = employeeDto.DepartmentId,
                /*Image = employeeDto.Image,*/
                LastModifiedBy = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            if(employeeDto.Image is not null)
            {
                employee.Image = _attachmentService.Upload(employeeDto.Image, "Images");
            }


            _unitOfWork.employeeRepository.Add(employee);
            return _unitOfWork.complete();
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
                DepartmentId = employeeDto.DepartmentId,

                LastModifiedBy = 1,
                CreatedBy = 1,
                LastModifiedOn = DateTime.UtcNow
            };

            _unitOfWork.employeeRepository.Update(employee);
            return _unitOfWork.complete();  
        }

        public bool DeleteEmployee(int id)
        {
            // i want the employeeRepository 2 times
            var employeeRepository = _unitOfWork.employeeRepository;

            var employee = employeeRepository.Get(id);
            if (employee is { })
            {
                // return _employeeRepository.Delete(employee) > 0;
                employeeRepository.Delete(employee);
            }
            return _unitOfWork.complete() > 0;
        }




    }
}
