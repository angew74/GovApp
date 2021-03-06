﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Threading;
using System.Threading.Tasks;
using Gov.Core.Identity;
using Gov.Structure.Config;
using Gov.Structure.Identity;
using Gov.Structure.Services;
using GovApp.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace GovApp.Controllers
{
    public class AccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [Authorize]
        public IActionResult MyProfile()
        {          
            return View();
        }

        private readonly UserStore _utentiService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationUserManager _userManager;
        private readonly ILogger<AccountController> _logger;
        private readonly IOptions<ComunicazioneConfig> _config;

        public AccountController(UserStore utentiService, SignInManager<ApplicationUser> SignInManager, ApplicationUserManager userManager, ILogger<AccountController> logger, IEmailSender emailSender, IOptions<ComunicazioneConfig> config)
        {
            _utentiService = utentiService;
            _userManager = userManager;
            _signInManager = SignInManager;
            _logger = logger;
            _emailSender = emailSender;
            _config = config;
        }



        [AllowAnonymous]
        public IActionResult Login()
        {
            string name = this.User?.Identity?.Name;         
            if (!string.IsNullOrEmpty(name))
            {
                CancellationToken cancellationToken = new CancellationToken();
                var user = _utentiService.FindByNameAsync(name, cancellationToken).Result;
                if (user.EmailConfirmed == true && user.LockoutEnabled == false)
                {
                    return Redirect("/GovApp/home/index");
                }
                else if(user.EmailConfirmed == false)
                {
                    return Redirect("/GovApp/account/confirmemail");
                }
                else if (user.LockoutEnabled)
                {
                    return Redirect("/GovApp/account/lockout");
                }
            }         
            return View();
        }

        [AllowAnonymous]
        public IActionResult AccessDenied()
        {
            ErrorModel errorViewModel = new ErrorModel();
            errorViewModel.errMsg = "Attenzione non sei abilitato a visualizzare questa pagina";   
            return View(errorViewModel);
        }
        [AllowAnonymous]
        public IActionResult ForgotPassword()
        {
            return View();
        }
        

        [Authorize]
        public IActionResult ConfirmEmail()
        {           
            string name = this.User?.Identity?.Name;
            ErrorModel error = new ErrorModel();
            CancellationToken cancellationToken = new CancellationToken();
            try
            {
                var user = _utentiService.FindByNameAsync(name, cancellationToken).Result;
                if (user == null || user.EmailConfirmed)
                {
                    return Redirect("/GovApp/account/accessdenied");
                }        
            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return RedirectToAction("/Error",error);
            }
            return View();
        }     


        [AllowAnonymous]
        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }


        [Authorize]
        public IActionResult ConfirmEmailConfirmation()
        {
            return View();
        }      

        [Authorize(Policy = "RequireUserRole")]
        public IActionResult ChangePassword()
        {
            ChangePasswordModel model = new ChangePasswordModel();
            var user = _userManager.GetUserAsync(User).Result;
            if (user == null)
            {
                return NotFound($"Impossibile aggiornare utente con ID '{_userManager.GetUserId(User)}'.");
            }
            model.UserName = user.UserName;
            return View(model);
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult manage()
        {
            return View();
        }

        [Authorize(Policy = "RequireAdministratorRole")]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "RequireUserRole")]
        public IActionResult Change(ChangePasswordModel model)
        {
            if (!ModelState.IsValid)
            {
                /*var errors = ModelState.Values.ToList();               
                 foreach (ModelStateEntry v in errors)
                 {
                     foreach (var e in v.Errors)
                     {                        
                         {
                             string key = ModelState.Keys.First();
                             ModelState.AddModelError(string.Empty, e.ErrorMessage);
                             ModelState.Remove(key);
                         }
                     }*
                 }*/
                return View("ChangePassword");
            }
            var user = _userManager.FindByNameAsync(model.UserName).Result;
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Utente non esistente");
                return View("ChangePassword");
            }
            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Impossibile aggiornare utente con ID " + user.Id);
                return View("ChangePassword");
            }
            var changePasswordResult = _userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword).Result;
            if (!changePasswordResult.Succeeded)
            {
                foreach (var error in changePasswordResult.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
                return View("ChangePassword");
            }
            _signInManager.RefreshSignInAsync(user);
            _logger.LogInformation("Utente ha cambiato password con successo.");
            return View("ChangePasswordConfirm");
        }
        [AllowAnonymous]
        public IActionResult Forgot(ForgotPasswordModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.FindByNameAsync(model.UserName).Result;
                if (user == null)
                {
                    ModelState.AddModelError(string.Empty, "Utente non esistente");
                    return View("ForgotPassword");
                }
                if (user.Email.ToLower() != model.Email.ToLower())
                {
                    ModelState.AddModelError(string.Empty, "Mail errata");
                    return View("ForgotPassword");
                }
                else
                {
                    string newpassword = PasswordGenerator.Generate();
                    user.Password = newpassword;
                    user.EmailConfirmed = false;
                    CancellationToken cancellationToken = new CancellationToken();
                    IdentityResult result = _utentiService.UpdateAsync(user, cancellationToken).Result;
                    if (result.Succeeded)
                    {
                        string body = _config.Value.BodyResetPassword + "<br /> UserName: " + user.UserName + " <br /> Password: " + user.Password;
                        _emailSender.SendEmailAsync(model.Email, _config.Value.SogggettoResetPassword, body);
                        return View("ForgotPasswordConfirmation");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Reset Password non effettuato riprovare in un secondo momento");
                        return View();
                    }

                }

            }
            else
            {
                return View("ForgotPassword");
            }
        }

        /*
        [AllowAnonymous]
        public IActionResult Reset(ResetPasswordModel model)
        {
            if (!ModelState.IsValid)
            { return View(); }
            var user = _userManager.FindByEmailAsync(model.Email).Result;
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToPage("./ResetPasswordConfirmation");
            }
            var result = _userManager.ResetPasswordAsync(user, model.Code, model.Password).Result;
            if (result.Succeeded)
            {
                return RedirectToPage("./ResetPasswordConfirmation");
            }
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return View();
        }*/


        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();           
            await HttpContext.SignOutAsync();
            _logger.LogInformation("Utente disconnesso");
            HttpContext.User = null;
            return View("logoutconfirmation");
        }

        [AllowAnonymous]
        public IActionResult logoutconfirmation()
        {
            return View();
        }
      

      
       
    }
}