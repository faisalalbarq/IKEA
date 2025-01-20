using LinkDev.IKEA.BusinesLogicLayer.Models.Departments;
using LinkDev.IKEA.DataAccessLayer.Models;
using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Departments;
using LinkDev.IKEA.DataAccessLayer.Persistence.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace LinkDev.IKEA.BusinesLogicLayer.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DepartmentService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public async Task <IEnumerable<DepartmentDto>> GetAllDepartmentsAsync()
        {

            var departments = await _unitOfWork.departmentRepository
                .GetAllAsIQueryable()
                .Where(D => !D.IsDeleted)
                .Select(department => new DepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate
            }).AsNoTracking().ToListAsync();

            return departments;


            //GetAll that return IEnumerable of Department 
            // وبترجعلي كل المعلومات من الداتا بيس بس انا مابدي الا بعض المعلومات 
            //IEnumerable of Department الحل انها ماترج
            // فبعمل فنكشن ثانيه 

            /// foreach (var department in departments)
            /// {
            ///     yield return new DepartmentToReturnDto
            ///     {
            ///             // manual mapping
            ///             Id = department.Id,
            ///             Code = department.Code,
            ///             Name = department.Name,
            ///             Description = department.Description,
            ///             CreationDate = department.CreationDate
            ///     };
            /// }

        }

        public async Task <DepartmentDetailsDto?> GetDepartmentByIdAsync(int id)
        {
            var department = await _unitOfWork.departmentRepository.GetAsync(id);

            if (department is not null) // or is { } new feature .net 8
                return new DepartmentDetailsDto()
                {
                    Id = department.Id,
                    Code = department.Code,
                    Name = department.Name,
                    Description = department.Description,
                    CreationDate = department.CreationDate,
                    CreatedBy = department.CreatedBy,
                    LastModifiedBy = department.LastModifiedBy,
                    LastModifiedOn = department.LastModifiedOn,
                };
            else
                return null;
        }


        public async Task <int> CreateDepartmentAsync(CreatedDepartmentDto departmentDto)
        {
            // manual mapping
            var department = new Department()
            {
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                //CreationDate = departmentDto.CreationDate,
                CreatedBy = 1,
                LastModifiedBy = 1,
                //CreatedOn = DateTime.UtcNow,
                LastModifiedOn = DateTime.UtcNow,

            };


            _unitOfWork.departmentRepository.Add(department);
            return await _unitOfWork.completeAsync();
        }

        public async Task<int> UpdateDepartmentAsync(UpdatedDepartmentDto departmentDto)
        {
            var department = new Department()
            {
                Id = departmentDto.Id,
                Code = departmentDto.Code,
                Name = departmentDto.Name,
                Description = departmentDto.Description,
                CreationDate = departmentDto.CreationDate,
                LastModifiedBy = 1,
                LastModifiedOn = DateTime.UtcNow,

            };


            _unitOfWork.departmentRepository.Update(department);
            return await _unitOfWork.completeAsync();
        }

        public async Task <bool> DeleteDepartmentAsync(int id)
        {
            var departmentRepository = _unitOfWork.departmentRepository;
            var department = await departmentRepository.GetAsync(id);

            if(department is { }) // if exist i will deleting 
                departmentRepository.Delete(department);

            return await _unitOfWork.completeAsync() > 0;
        }


    }
}
