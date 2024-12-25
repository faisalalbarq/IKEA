using LinkDev.IKEA.PresentationLayer.Models.Common;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LinkDev.IKEA.PresentationLayer.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        // ILogger: 
        //الي صارت عنا  logging to exceptions المسؤول اننا نعمل 
        // وما بنعتمد عليه لانه بنحمل مكتبه كامله لهذا الموضوع 
        //logging system microsoft والمكتبه او الباكج فيه مميزات اكثر واحسن من ال 
        // المشاكل الي عنا محتاجين نعمللها لوجنج بالداتا بيس او فايل خارجي عشان تيم السبورتيد يشوف المشاكل ويحلها 


        {
            _logger = logger;
        }

       
        public IActionResult Index()
        {
            return View(); // بوديني على الفيو اندكس 
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() 
            //بانه انا لو ماكنت بالديفيلوبمنت انفايرونمنت المعينه وصارت مشكله فيهندل الاكسيبشن فيه program.cs  بستخدمه بال
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            // shared برجع فيو موجود بال
            // ErrorViewModel موجود بفولدر المودل
            //viewModels الي لازم يكون اسمه 
        }
    }
}
