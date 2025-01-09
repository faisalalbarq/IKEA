using LinkDev.IKEA.BusinesLogicLayer.Models.Departments;
using LinkDev.IKEA.BusinesLogicLayer.Services.Departments;
using LinkDev.IKEA.DataAccessLayer.Models.Department;
using LinkDev.IKEA.PresentationLayer.ViewModels.Departments;
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
        #region Services
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
        #endregion

        #region Index
        [HttpGet] //GET: /Department/index (default action is index) 
        public IActionResult Index()
        {
            var department = _departmentService.GetAllDepartments();


            return View(department);
        } 
        #endregion

        #region Create
        [HttpGet]// GET: /Department/Create
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(DepartmentViewModel departmentVM)
        {
            if (!ModelState.IsValid) // Server-Side-Validation
                return View(departmentVM);
            // ModelState: هاي اوبجكت ورثناها وبتكون فاليد يعني ترو لمه البيانات الي بالديبارتمنت باراميتر تكون فاليد بالنسبه للكونديشنز الي بال دتو تبعها 

            string msg = string.Empty;
            try
            {
                var departmentToCreate = new CreatedDepartmentDto()
                {
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate
                };

                var result = _departmentService.CreateDepartment(departmentToCreate);// number of records

                if (result > 0)
                    return RedirectToAction("Index");
                else
                {
                    msg = "Department is Not Created";
                    ModelState.AddModelError(string.Empty, msg);
                    return View(departmentVM);
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
                    return View(departmentVM);
                }
                else
                {
                    msg = "Department is Not Created";
                    return View("Error", msg);
                }
            }
        } 
        #endregion

        #region Details
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
        #endregion

        #region Update
        [HttpGet] //GET: /Department/Edit/id
        public IActionResult Edit(int? id)
        {
            if (id is null) return BadRequest(); // 400

            var department = _departmentService.GetDepartmentById(id.Value);
            if (department is null)
                return NotFound(); // 404

            return View(new DepartmentViewModel()
            {
                Code = department.Code,
                Name = department.Name,
                Description = department.Description,
                CreationDate = department.CreationDate
            });



        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit([FromRoute] int id, DepartmentViewModel departmentVM)
        {
            // ModelState: is contain the data that i'm submited and when the model(departmentVM) come to read the his value, valid or invalid now
            if (!ModelState.IsValid)
            {
                return View(departmentVM);
            }

            var msg = string.Empty;
            try
            {
                var departmentToUpdate = new UpdatedDepartmentDto()
                {
                    Id = id,
                    Code = departmentVM.Code,
                    Name = departmentVM.Name,
                    Description = departmentVM.Description,
                    CreationDate = departmentVM.CreationDate
                };


                var Updated = _departmentService.UpdateDepartment(departmentToUpdate) > 0;
                if (Updated)
                {
                    return RedirectToAction("Index");
                }
                msg = "An Error has occured during the updating the department";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                msg = _environment.IsDevelopment() ? ex.Message : "An Error has occured during the updating the department";

                /// if (_environment.IsDevelopment())
                /// {
                ///     msg = ex.Message;
                ///     return View(department);
                /// }
                /// else
                /// {
                ///     msg = "An Error has occured during the updating the department";
                ///     return View("Error", msg);
                /// }
            }
            ModelState.AddModelError(string.Empty, msg);
            return View(departmentVM);
        } 
        #endregion

        #region Delete
        [HttpGet] //GET: Department/Delete/id
        public IActionResult Delete(int? id)
        {
            // same details
            if(id is null)
            {
                return BadRequest();
            }

            var department = _departmentService.GetDepartmentById(id.Value);

            if(department is null)
            {
                return NotFound(); // helper method
            }
            return View(department);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            var msg = string.Empty;

            try
            {
                var deleted = _departmentService.DeleteDepartment(id);
                if (deleted)
                    return RedirectToAction("Index");

                msg = "An Error has occured during the deleteing the department";
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);

                msg = _environment.IsDevelopment() ? ex.Message : "An Error has occured during the deleting the department";
            }
            //ModelState.AddModelError(string.Empty, msg);
            return RedirectToAction("Index");
        } 
        #endregion

    }
}
