using LinkDev.IKEA.BusinesLogicLayer.Models.Departments;
using LinkDev.IKEA.DataAccessLayer.Models.Department;
using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Departments;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.BusinesLogicLayer.Services.Departments
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepositry;
         
        public DepartmentService(IDepartmentRepository departmentRepositry)
        {
            _departmentRepositry = departmentRepositry;
        }



        public IEnumerable<DepartmentDto> GetAllDepartments()
        {

            var departments = _departmentRepositry.GetAllAsIQueryable().Select(department => new DepartmentDto()
            {
                Id = department.Id,
                Code = department.Code,
                Name = department.Name,
                CreationDate = department.CreationDate
            }).AsNoTracking().ToList();

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

        public DepartmentDetailsDto? GetDepartmentById(int id)
        {
            var department = _departmentRepositry.Get(id);

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


        public int CreateDepartment(CreatedDepartmentDto departmentDto)
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
            return _departmentRepositry.Add(department);
        }

        public int UpdateDepartment(UpdatedDepartmentDto departmentDto)
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


            return _departmentRepositry.Update(department);

        }

        public bool DeleteDepartment(int id)
        {
            var department = _departmentRepositry.Get(id);

            if(department is { }) // if exist i will deleting 
                return _departmentRepositry.Delete(department) > 0;

            return false;
        }


    }
}
