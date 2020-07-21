using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GovApp.Controllers
{
    public class AndamentoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Inserimento()
        {
            return View();
        }
    }
}