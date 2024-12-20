using LinkDev.IKEA.BusinesLogicLayer.Services.Departments;
using Microsoft.AspNetCore.Mvc;

namespace LinkDev.IKEA.PresentationLayer.Controllers
{
    public class DepartmentController : Controller
    {
        /// Controller relationships: 
        /// 1- Inheritance: DepartmentController is  a Controller
        /// 2- Composition: DepartmentController has a IDepartmentService




        //DepartmentService هون بدي اتعامل مع كلاس 
        //IDepartmentService فانا بقدر اطلب اوبجكت من كلاس بامبليمنت 
        // بثلاث طرق على مستوى الكونتلرولير 
        /// 1- 
        ///[FromServices]
        ///public IDepartmentService DepartmentService { get; } = null!;

        //2-
        private readonly IDepartmentService _departmentService; // مابخليها نلابل عشان بحتاجها , هيك مبدا الكومبوزيشن
        public DepartmentController(IDepartmentService departmentService) //([FromService]IDepa...) الديفوت انه مكتوب 
        {
            _departmentService = departmentService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
