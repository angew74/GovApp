﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace GovApp.Controllers
{
    public class GovernoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}