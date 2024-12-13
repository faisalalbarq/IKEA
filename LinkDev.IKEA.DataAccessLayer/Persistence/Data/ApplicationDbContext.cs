using LinkDev.IKEA.DataAccessLayer.Models.Department;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace LinkDev.IKEA.DataAccessLayer.Persistence.Data
{
    public class ApplicationDbContext : DbContext
    {
        //ctor
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            /*
            parameter less ctor عن طريق الApplicationDbContext بعمل اوبجكت من كلاس  
            parameter less ctor base وهذا بنادي ال 
            DbContextOptions وبوخذ مني اوبجكت من كلاس base موجود بالctor  وهذا بنادي على 
            DbContextOptions  عشان نزبط الاوبجكت نفسه تبع ال onConfiguring  الاخير هو الي بفتح كونكشن مع الداتابيس بس قبلها بكول ال ctor  ال 
            رح يلاقيها فاضيه فلازم اعمللها اوفررايدDbContext تبع ال onConfiguring  المهم لمه يكول ال 
             */

            /*
             Dependency injection  بس هسه بدي استخدم طريقه ال 
             DbContextOptions فلازم احكي انه الي بده يعمل اوبجكت منه لازم يعطيه اوبجكت من كلاس ApplicationDbContext  وهيه اني احكي انه اي واحد بده يعمل اوبجكت من كلاس 
             يعني بدي مايكون في اني اروح عالبيس كلاس الي هو اصلا بس كوبري 
             ApplicationDbContext تبعنا الي هوه DbContext بدي اياه يكون اوف ال  DbContextOptions وطبعا مش بس 
             parameter less ctor وبدال ما انا بنادي عال 
             بنادي مره وحده عال ابوه 
             */

            /*
             هاي الاوبشنز مش كونفيجر يعني ماحددتلها الكولكشن سترنج
             DbContext هو الي رح اطلب منه يملي اوبجكت من كلاس  clr لانه ال 
             ومش بعمله انا ؟clr ليش بطلب من ال
             DbContextOptions عشان لو انا بنفس الريكويست واحتجت يكون معاي امبلويي وديبارتمنت وبرودكت ريبوستري فانا بكلم نفس ال 
             DbContextOptions ورح يلاقيه انه بعتمد على ApplicationDbContext فرح يعمل اوبجكت من كلاس  heap انه يعمله ويحطه بال clr  فبطلب من 
            ووقتها رح اكونفجر الاوبشنز فمش محتاج فنكشن الاون كونفجر  enable dependency injection for dbcontextoption فمحتاج اني 
           ورح يعمله اياه بس محتاج  ApplicationDbContext فلمه حدا بده اوبجكت من اي ريبو فبده اوبجكت من كلاس 
            Allow dependency injection for ApplicationDbContext 
            الي بتعتمد عليه كل الريبوز 
           ApplicationDbContext الي بتعتمد عليه ال DbContextOptions ومحتاج كمان نفس الحكي لل 
            فبعمل الحكي بالبروجرام باول جزء 
             */
        }
        /*        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //base.OnConfiguring(optionsBuilder);
            optionsBuilder.UseSqlServer("Server = .; Database = IKEA; Trusted_Connection = True; TrustServerCertificate = True;");
            // MultipleActiveResultSets = True
        }*/

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            /*
              base.OnModelCreating(modelBuilder);
              فبالتالي مافي اي مودل فبالتالي مافي اي فلوونت ابأي مكتوبه DbSet ماعنده كود لانه ماعنده DbContext  مابعمل للاب كول عشان ال  
             */
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
        public DbSet<Department> Departments { get; set; }
    }
}
 