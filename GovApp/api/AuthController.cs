using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Gov.Core.Identity;
using Gov.Structure.Config;
using GovApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity.UI.V3.Pages.Account.Internal;

namespace GovApp.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserStore<ApplicationUser> _utentiService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ILogger<ChangePasswordModel> _logger;
        private readonly IOptions<ComunicazioneConfig> _config;
        public AuthController(IUserStore<ApplicationUser> utentiService, SignInManager<ApplicationUser> SignInManager, UserManager<ApplicationUser> userManager, ILogger<ChangePasswordModel> logger, IEmailSender emailSender, IOptions<ComunicazioneConfig> config)
        {
            _utentiService = utentiService;
            _userManager = userManager;
            _signInManager = SignInManager;
            _logger = logger;
            _emailSender = emailSender;
            _config = config;
        }

        public class Input
        {
            public Credentials credentials { get; set; }
            public ConfirmEmail confirm { get; set; }
        }

        public class Credentials
        {


            public string username { get; set; }
            public string password { get; set; }

        }


        [AllowAnonymous]
        [HttpPost("login")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Login([FromBody] Input credentials)
        {
            ApplicationUser user = new ApplicationUser();
            ErrorModel error = new ErrorModel();
            if (ModelState.IsValid)
            {
                CancellationToken cancellationToken = new CancellationToken();
                try
                {
                    user = _utentiService.FindByNameAsync(credentials.credentials.username, cancellationToken).Result;
                    if (user != null)
                    {
                        await _signInManager.SignOutAsync();
                        if (user.LockoutEnabled)
                        {
                            _logger.LogError("Utenza Bloccata " + credentials.credentials.username);
                            error.errMsg = "Utenza Bloccata";
                            return BadRequest(error);
                        }
                        Microsoft.AspNetCore.Identity.SignInResult result =
                               _signInManager.PasswordSignInAsync(
                                  user, credentials.credentials.password, false, false).Result;     
                        if (result.Succeeded)
                        {
                            var principal = await _signInManager.CreateUserPrincipalAsync(user);
                            await HttpContext.SignInAsync(principal);
                            if (user.EmailConfirmed == false)
                            {
                                error.Url = "/account/confirmemail";
                                error.errMsg = "vai email da confermare";
                                return BadRequest(error);
                            }
                            else
                            {
                                return Ok(new
                                {
                                    name = principal?.Identity?.Name,
                                    email = principal?.FindFirstValue(ClaimTypes.Email),
                                    role = principal?.FindFirstValue(ClaimTypes.Role),
                                });
                            }
                        }
                        else
                        {
                            _logger.LogError("UserName o password errata" + credentials.credentials.username);
                            error.errMsg = "UserName o password errata";
                            return BadRequest(error);
                        }
                    }
                    _logger.LogError("UserName o password errata" + credentials.credentials.username);
                    error.errMsg = "UserName o password errata";
                    return BadRequest(error);

                }
                catch (Exception ex)
                {
                    _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                    error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                    return BadRequest(error);
                }
            }
            error.errMsg = "Errore di validazione";
            return Ok(error);
        }


        [AllowAnonymous]
        [HttpGet("context")]
        public IActionResult Context()
        {           
            return Ok(new
            {
                name = this.User?.Identity?.Name,
                email = this.User?.FindFirstValue(ClaimTypes.Email),
                role = this.User?.FindFirstValue(ClaimTypes.Role),
            });
        }

        [Authorize]
        [HttpGet("confirm")]
        public async Task<IActionResult> Confirm()
        {
            ConfirmEmail model = new ConfirmEmail();
            string name = this.User?.Identity?.Name;
            ErrorModel error = new ErrorModel();
            CancellationToken cancellationToken = new CancellationToken();
            try
            {
                var user = await _utentiService.FindByNameAsync(name, cancellationToken);
                if (user != null)
                {
                    model = new ConfirmEmail
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        Id = user.Id.ToString()
                    };
                }
                else
                {
                    return NotFound($"Impossibile caricare un utente con Username '{name}'.");
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }
            return Ok(model);
        }

        [Authorize]
        [HttpPost("confirmation")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Confirmation([FromBody] Input model)
        {
            model.confirm.Result = false;
            ErrorModel error = new ErrorModel();
            if (!ModelState.IsValid)
            {
                error.errMsg = "Errore di validazione";
                return BadRequest(error);
            }
            try
            {
                var user = _userManager.FindByIdAsync(model.confirm.Id).Result;
                if (user == null)
                {
                    _logger.LogError("Impossibile aggiornare utente con ID '{model.confirm.Id}'.");
                    error.errMsg = "Impossibile aggiornare utente con ID '{model.confirm.Id}'.";
                    return BadRequest(error);                   
                }
                user.Password = model.confirm.Password;
                user.EmailConfirmed = true;
                CancellationToken cancellationToken = new CancellationToken();
                var result = await _utentiService.UpdateAsync(user, cancellationToken);
                if (result.Succeeded)
                {
                    model.confirm.Url = "/account/ConfirmEmailConfirmation";
                    model.confirm.Result = true;
                }
                else
                {
                    _logger.LogError("Errore nell'aggiornamento dei dati");
                    error.errMsg = "Errore nell'aggiornamento dei dati";
                    return BadRequest(error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }          
            return Ok(model);
        }
    }
}