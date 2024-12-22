using LinkDev.IKEA.BusinesLogicLayer.Models.Departments;
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
        private readonly ILogger<DepartmentController> _logger;
        private readonly IWebHostEnvironment _environment;

        public DepartmentController(
            IDepartmentService departmentService,
            ILogger<DepartmentController> logger,
            IWebHostEnvironment environment) //([FromService]IDepa...) الديفوت انه مكتوب 
        {
            _departmentService = departmentService;
            _logger = logger;
            _environment = environment;  
        }

        [HttpGet] //GET: /Department/index (default action is index) 
        public IActionResult Index()
        {
            var department = _departmentService.GetAllDepartments(); 


            return View(department);
        }

        [HttpGet]// GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreatedDepartmentDto department)
        {
            if (!ModelState.IsValid) // Server-Side-Validation
                return View(department);
            // ModelState: هاي اوبجكت ورثناها وبتكون فاليد يعني ترو لمه البيانات الي بالديبارتمنت باراميتر تكون فاليد بالنسبه للكونديشنز الي بال دتو تبعها 

            string msg = string.Empty;
            try
            {
                var result = _departmentService.CreateDepartment(department);// number of records

                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    msg = "Department is Not Created";
                    ModelState.AddModelError(string.Empty, msg);
                    return View(department);
                }
            }
            catch (Exception ex)
            {
                //1. log Exception using serial log package 
                // using logging system .Net: i need obj from class that implement ILogger
                _logger.LogError(ex, ex.Message);

                //2. Set Message :
                //if i'm in development Environment i need the technical message 
                //if i'm in production  Environment i will sent the friendly msg
                //Checking Environments: i need obj from class that implement IWebHostEnvironment

                if (_environment.IsDevelopment())
                {
                    msg = ex.Message;
                    return View(department);    
                }
                else
                {
                    msg = "Department is Not Created";
                    return View("Error", msg);
                }


                throw;
            }
        }


        [HttpGet] //GET: /Department/Details/id
        public IActionResult Details(int? id)
        {
            if (id is null)
                return BadRequest();

            var department = _departmentService.GetDepartmentById(id.Value);

            if (department is null)
                return NotFound();

            return View(department);
        }


    }
}
