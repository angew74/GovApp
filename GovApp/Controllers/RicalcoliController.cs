using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GovApp.Controllers
{
    public class RicalcoliController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Liste()
        {
            return View();
        }
        public IActionResult Sindaco()
        {
            return View();
        }
        public IActionResult Prefenze()
        {
            return View();
        }
    }
}