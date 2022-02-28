using Kaptcha.Builder;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Test.Models;

namespace Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {

            var aa = KaptchaBuilder.Init().UseNoisy().UseCustomNumbers(1,3,5,7).UsePersianNumbers().Mode(Kaptcha.Model.Enums.Mode.Medium).Build();
           // return File(aa.ImageFile.FileContents, aa.ImageFile.ContentType);
            return Ok(aa);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}