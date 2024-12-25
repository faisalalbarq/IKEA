using LinkDev.IKEA.BusinesLogicLayer.Models.Departments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
    
namespace LinkDev.IKEA.BusinesLogicLayer.Services.Departments
{
    public interface IDepartmentService
    {
        // IEnumerable<Department> GetAllDepartments(); ==> 
        // غلط اني ارجع هذا الانترفيس لانه مش كل البروبرتيس بدي اعرضها فلازم اعمل كلاس يمثل شكل الداتا الي رح اظهرها
        //DTO(Data transfer object) اسمه 
        //(DAL,BLL)هو الاوبجكت المسؤول عن انه يشيل الداتا الي بصيرلها ترانسفير مابين اللايرز
        // or (frontend , backend) such api
        //بعمل كلاس اسمه مودلز او دتوBLL جوه ال 


        IEnumerable<DepartmentDto> GetAllDepartments();// getAll

        DepartmentDetailsDto? GetDepartmentById(int id); //getByid

        int CreateDepartment(CreatedDepartmentDto departmentDto);

        int UpdateDepartment(UpdatedDepartmentDto departmentDto);

        bool DeleteDepartment(int id);
    }
}
