using ChooseDelice.Data;
using ChooseDelice.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using System.Diagnostics;
using System.Xml.Linq;

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
          
            IList <Question>qdata = deliceContext.Questions.ToList();
            var ddata = deliceContext.Delices.ToList();
            var pdata = deliceContext.PartialDecisions.ToList();


            ViewData["intrebari"] = qdata;

            return View();
        }

        [HttpPost]
        public ActionResult Submit(Question model)
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