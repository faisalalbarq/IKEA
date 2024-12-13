using LinkDev.IKEA.DataAccessLayer.Persistence.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LinkDev.IKEA.PresentationLayer
{
    public class Program
    {
        public static void Main(string[] args)
        {
        
            #region Configure Services 
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            #region AddDbContext بدال ما اكتب كل هذا في فنكشن اسمها 
            /// builder.Services.AddScoped<ApplicationDbContext>();
            /// builder.Services.AddScoped<DbContextOptions<ApplicationDbContext>>((ServiceProvider) =>
            /// {
            ///     var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
            ///     optionsBuilder.UseSqlServer("");
            /// 
            ///     var options = optionsBuilder.Options;
            ///     return options;
            /// 
            /// 
            ///     //return new DbContextOptions<ApplicationDbContext>();
            ///     // بستخدم هذا الاوفرلود لو انا بدي اعمل الاوبجكت عشان اغير اشي معين 
            /// });
            #endregion

            #region AddDbContext بكتب الصيغه الثانيه اختصار لل  
            ///builder.Services.AddDbContext<ApplicationDbContext>
            ///    (
            ///    
            ///    contextLifetime: ServiceLifetime.Scoped,
            ///    optionsLifetime: ServiceLifetime.Scoped,
            ///    optionsAction: (OptionsBuilder) =>
            ///    {
            ///        OptionsBuilder.UseSqlServer("");
            ///    }
            ///
            ///    // optionsAction : action of one parameter (don't return any thing) & parameter name is:
            ///    //                 DbContextOptionBuilder this same optionBuilder
            ///    );

            // الصيغه الثانيه : 
            builder.Services.AddDbContext<ApplicationDbContext>((OptionsBuilder) =>
            {
                // OptionsBuilder.UseSqlServer(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]);
                // or


                OptionsBuilder.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
               
                
                /*
                     بهاي الطريقه مش صح connection string  كتابتي لل 
                     الي بتتغير بالابليكيشن من انفايرونمنت للثانيه App settings  من ال  connection string لانه ال 
                     Encripted وتكون  App settings فبنكتبها بال 
                     connection string  وبكتب   App settings فبروح عال 
                   Iconfigration عن طريق ال  App settings  وعشان اقراءه فلازم اكلم ال 
                */

            });


            #endregion

           
        
            
            
            #endregion

            var app = builder.Build();


            #region Configure Kestrel Middelwears
            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            /*app.UseAuthorization();*/

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            app.Run();
        }
    }
}
