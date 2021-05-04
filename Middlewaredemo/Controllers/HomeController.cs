using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Middlewaredemo.Controllers
{
 
    public class HomeController : Controller
    {
        //if Id is specefied in route then this will do specefit routing
        //this is an optional param
        [ModelBinder]
        public string Id { get; set; }
        public IActionResult Index()
        {
            return View();
        }
        [Route("/contact-us", Name = "Contact")]
        public IActionResult Contact()
        {
            return View();
        }
    }
}
