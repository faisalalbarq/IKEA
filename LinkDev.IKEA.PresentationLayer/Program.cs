using LinkDev.IKEA.BusinesLogicLayer.Common.Services.Attachments;
using LinkDev.IKEA.BusinesLogicLayer.Services.Departments;
using LinkDev.IKEA.BusinesLogicLayer.Services.Employees;
using LinkDev.IKEA.DataAccessLayer.Models.Identity;
using LinkDev.IKEA.DataAccessLayer.Persistence.Data;
using LinkDev.IKEA.DataAccessLayer.Persistence.UnitOfWork;
using LinkDev.IKEA.PresentationLayer.Mappring;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

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


                OptionsBuilder
                .UseLazyLoadingProxies()
                .UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
               
                
                /*
                     بهاي الطريقه مش صح connection string  كتابتي لل 
                     الي بتتغير بالابليكيشن من انفايرونمنت للثانيه App settings  من ال  connection string لانه ال 
                     Encripted وتكون  App settings فبنكتبها بال 
                     connection string  وبكتب   App settings فبروح عال 
                   Iconfigration عن طريق ال  App settings  وعشان اقراءه فلازم اكلم ال 
                */

            });
            // Allow the dependency injection
            // builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            // builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddTransient<IAttachmentService, AttachmentService>();

            builder.Services.AddScoped<IDepartmentService , DepartmentService>();
            builder.Services.AddScoped<IEmployeeService, EmployeeService>();

            builder.Services.AddAutoMapper(M => M.AddProfile(new MappingProfile()));//اوبجكت منه فييجيني واستخدمه ولكن هذا الاوبجكت لازم يكون عارف كيف يحول من كذا ل كذا فلازم اعمله بروفايل  clr عشان لمه اطلب من ال 
            //AddAutoMapper => الباراميتر هو انه مستني مني انه المابر اوبجكت الي هو رح يعمله محتاج اضيف جواته بروفايلز عشان يعرف يشتغل
            // فهو رح ينفذ الكونستركتور الي بكلاس المابنق بروفايل





            /// builder.Services.AddScoped<UserManager<ApplicationUser>>();
            /// builder.Services.AddScoped<SignInManager<ApplicationUser>>();
            /// builder.Services.AddScoped<RoleManager<IdentityRole>>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>();
			// AddIdentity => adds the default identity system configuration, that maens:
			// 1- this method go to register the three main services for identity with the dependencies that have
			// 2- add congigurations specific the security
			// 2- add the default identity system configuration
			// this means, i don't need to call the three services.
			// if i want to know the configurations that this method do, i will using the second overload:
			// take action with type IdentityOption, because i have set of options if i need to make configure for this options
			/*
             * builder.Services.AddIdentity<ApplicationUser, IdentityRole>((options) =>{
             * options.password.Requirelength = 5; ==> the default is 6
             * options.password.RequireNonAlphanumeric = true; ==> the default is 
             * options.password.RequireUppercase = true; ==> the default is 
             * optinos.password.RequireDigit = true; ==> the default is 
             * options.password.RequireLowercase = true; ==> the default is 
             * options.password.RequiredUniqueChars = 3; ==> the default is 1 // the number of unique characters in the password 
             * 
             * options.user.RequireUniqueEmail = true; ==> the default is true
             * options.user.AllowedUserNameCharacters = "abc"; ==> the default is "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+"
             
			 * options.lockout.AllowedForNewUsers = true; ==> the default is true
			 * 
			 * options.lockout.maxFailedAccessAttempts = 5; ==> the default is 5
			 * options.lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5); ==> the default is 5 minutes
             // these two lockout options: if user try to login 5 times and he failed, he will be locked account for 5 minutes 
			 * });
             */

			builder.Services.ConfigureApplicationCookie(options =>
			{
				options.LoginPath = "/Account/SignIn";
                // options.LogoutPath = "/Account/SignIn";
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

            app.UseAuthentication();
            app.UseAuthorization();

            /*app.UseAuthorization();*/

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            #endregion

            app.Run();
        }
    }
}
