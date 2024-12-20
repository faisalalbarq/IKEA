using LinkDev.IKEA.DataAccessLayer.Models.Department;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Departments
{
    public interface IDepartmentRepositry
    {
        // اي ريبستري بكون فيه 5 فنكشنز اساسيه 
        // getAll, getById, create, update,Delete
        // هدف وجود الانترفيس هذا اشيين : 
        // اقدر ابعث اوبجكت من اي كلاس بامبليمنت هذا الانترفيس للسيرفس 
        // وعشان اقدر اعمل موكنج للسيرفس من غير ما اجي ناحيه الريبوستري 

        IEnumerable<Department> GetAll(bool withAsNoTracking = true); //getAll
        ///withAsNoTracking means: 
        ///هل انا بدي اعمل تراكنج للاوبجكت ولا لا وانا مابدي اصلا اعمل تراكنج لانه هو بدي اعرض بيانات بس
        /// لاني مش بدولفد اجانست انترفيس ولازم يكون عندي جواته ليستList مابستخدم 
        ///لاني هيك بصير متاح انه يفوت يعدل ويعمل اي اوبراشن/ ICollection وما بستخدم 
        ///IEnumerable بستخدم 
        
        IQueryable<Department> GetAllAsIQueryable();



        Department? Get(int id); // getById


        int Add(Department entity); // create
        //create بسميها هيك لانه الي جوه السيرفس اسمها 

        int Update(Department entity); // update

        int Delete(Department entity); // Delete

        /// اخر 3 برجعو انتيجر لاني بدي عدد الروز الي صارلها تغيير 
    }
}
