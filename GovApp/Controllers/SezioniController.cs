﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GovApp.Controllers
{
    public class SezioniController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Status()
        {
            return View();
        }
    }
}
