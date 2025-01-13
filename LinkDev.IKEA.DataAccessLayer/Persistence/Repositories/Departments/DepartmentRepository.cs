using LinkDev.IKEA.DataAccessLayer.Models;
using LinkDev.IKEA.DataAccessLayer.Persistence.Data;
using LinkDev.IKEA.DataAccessLayer.Persistence.Repositories._Genegic;

namespace LinkDev.IKEA.DataAccessLayer.Persistence.Repositories.Departments
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }


        ///        
        ///        // كل ريبوستري مسؤول انه يتعامل مع تيبل معين فلازم يتعامل مع الكلاس المسؤول عن الداتا بيس 
        ///        
        ///        //ApplicationDbContext يتعامل مع 
        ///        private readonly ApplicationDbContext _dbContext; // feild 
        ///
        ///        public DepartmentRepository(ApplicationDbContext dbContext) {
        ///            /// كل الميثود هذول هم اوبجكت ميمبر ولازم يكون في اوبجكت من الريبوستري عشان انفذها 
        ///            /// ولمه اعمل اوبجكت فلازم افتح كوننيكشن عشان اعمل الميثودز 
        ///
        ///            ///_dbContext = new ApplicationDbContext();
        ///            /// dbContextOptions هاي غلط لانه معتمد على اوبجكت من 
        ///            ///يعمل ابوجكت clr فلازم اطلب من ال 
        ///            /// في 5 طريق عشان اطلب ورح نجرب هسه طريقتين 
        ///            /// الاولى انه هل انا بدي الوبجكت يكون متاح على الكلاس كله يعني كل الميثود الي بالكلاس محتاجيته 
        ///            /// لو الجواب اه فبنطلب (Implicitly)بالكوستركتور كالاتي 
        ///            ///  (ApplicationDbContext dbContext)
        ///            /// Dependency injection المفروض بكل لاير يكون في كلاس لل 
        ///            /// 
        ///
        ///            ///dbContext لانه كرييشن الاوبجكت من كلاس ال ريبوستري بعتمد على انجكشن من اوبجكت من ال  Dependency injection اسمها 
        ///            ///_dbContext = dbContext جوه الانستركتور اكتب 
        ///            
        ///            _dbContext = dbContext;
        ///
        ///            ///لو بدي يكون متاح على ميثود معينه فبكتب بالبراميتر تبعتها 
        ///            /// ([FromServices] ApplicationDbContext)
        ///        }
        ///
        ///
        ///        public IEnumerable<Department> GetAll(bool withAsNoTracking = true)
        ///        {
        ///            if (withAsNoTracking)
        ///            {
        ///                return _dbContext.Departments.AsNoTracking().ToList();
        ///            }
        ///            return _dbContext.Departments.ToList();
        ///        }
        ///
        ///        public IQueryable<Department> GetAllAsIQueryable()
        ///        {
        ///            return _dbContext.Departments;
        ///            //ومافي اشي اتنفذ هون  Queryable هون انا برجع دب سيت وهو عباره من 
        ///            //لمه استخدم اميديات اوبيراتور 
        ///            //.GetAllAsIQueryable().Select(D => new DepartmentToReturnDto() بكتب GetAll() فبكتب بالديبارتمنت سيرفز بدل 
        ///        }
        ///
        ///        public Department? Get(int id)
        ///        {
        ///            // Find ==> بتدور لوكالي بعدين بتجيب من الداتا بيس
        ///            return _dbContext.Departments.Find(id);
        ///
        ///            /// الطريقه القديمه
        ///            /// var Department = _dbContext.Departments.Local.FirstOrDefault(D => D.Id == id);
        ///            /// 
        ///            /// // .Local ==> عشان اول اشي يدور باللوكال لو جاب هذا الاوبجكت من قبل ولا لا 
        ///            /// if(Department is null)
        ///            /// {
        ///            ///     Department = _dbContext.Departments.FirstOrDefault(D => D.Id == id);
        ///            ///     // لو مش موجود باللوكال فيجيبه من الداتا بيس
        ///            /// }
        ///            /// return Department;
        ///
        ///
        ///            /// سؤال مهم
        ///            /// انا جبت الديبارتمنت وحطيته عندي باللوكلي بعدين صارله ابديت  
        ///            /// فهل لمه اناديه مره ثانيه رح يسمع الابديت فيه ولا لا
        ///        }
        ///
        ///        public int Add(Department entity)
        ///        {
        ///            _dbContext.Departments.Add(entity);
        ///            return _dbContext.SaveChanges();    
        ///        }
        ///
        ///        public int Update(Department entity)
        ///        {
        ///            _dbContext.Departments.Update(entity);
        ///            return _dbContext.SaveChanges();
        ///        }
        ///
        ///        public int Delete(Department entity)
        ///        {
        ///            _dbContext.Departments.Remove(entity);
        ///            return _dbContext.SaveChanges();
        ///        }
        ///
        ///

    }
}
