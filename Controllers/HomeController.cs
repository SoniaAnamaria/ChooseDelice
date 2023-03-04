using ChooseDelice.Data;
using ChooseDelice.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ChooseDelice.Controllers
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
            ChooseDeliceContext deliceContext = new();
            var qdata = deliceContext.Questions.ToList();
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}