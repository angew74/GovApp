using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GovApp.Models;
using Microsoft.AspNetCore.Authorization;
using Gov.Core.Identity;
using Microsoft.AspNetCore.Identity;
using System.Threading;

namespace GovApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUserStore<ApplicationUser> _utentiService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(ILogger<HomeController> logger, IUserStore<ApplicationUser> utentiService, SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _utentiService = utentiService;
            _userManager = userManager;
            _signInManager = SignInManager;
        }
        [Authorize(Policy = "RequireUserRole")]
        public IActionResult Index()
        {
            string name = this.User?.Identity?.Name;
            ErrorModel error = new ErrorModel();
            CancellationToken cancellationToken = new CancellationToken();
            try
            {
                var user = _utentiService.FindByNameAsync(name, cancellationToken).Result;
                if (user.EmailConfirmed == false)
                {
                    return Redirect("/account/confirmemail");
                }
                if (user.LockoutEnabled)
                {
                    return View("/Account/lockout");
                }
            }
            catch(Exception ex)
            {               
                error.errMsg = "Errore grave: " + ex.Message;
                _logger.LogError(error.errMsg);
               return RedirectToAction("Error", error);
                
            }
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
