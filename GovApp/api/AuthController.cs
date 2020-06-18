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
using Gov.Structure.Identity;
using Gov.Core.Enumerators;
using Gov.Structure.Extensions;

namespace GovApp.api
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserStore _utentiService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly ApplicationUserManager _userManager;
        private readonly ILogger<AuthController> _logger;
        private readonly IOptions<ComunicazioneConfig> _mailConfig;
        private readonly IOptions<PagingConfig> _pagingConfig;
        private readonly IOptions<IdentityOptions> _identityOptions;
        public AuthController(UserStore utentiService, SignInManager<ApplicationUser> SignInManager, ApplicationUserManager userManager, ILogger<AuthController> logger, IEmailSender emailSender, IOptions<ComunicazioneConfig> config, IOptions<PagingConfig> pagingConfig, IOptions<IdentityOptions> identityOptions)
        {
            _utentiService = utentiService;
            _userManager = userManager;
            _signInManager = SignInManager;
            _logger = logger;
            _emailSender = emailSender;
            _mailConfig = config;
            _pagingConfig = pagingConfig;
            _identityOptions = identityOptions;
        }

        public class Input
        {
            public Credentials credentials { get; set; }
            public Confirm confirm { get; set; }
            public Confirm change { get; set; }
            public UserModel user { get; set; }
            public SortingModel sort { get; set; }
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
            Confirm model = new Confirm();
            string name = this.User?.Identity?.Name;
            ErrorModel error = new ErrorModel();
            CancellationToken cancellationToken = new CancellationToken();
            try
            {
                var user = await _utentiService.FindByNameAsync(name, cancellationToken);
                if (user != null)
                {
                    model = new Confirm
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

        [Authorize]
        [HttpPost("register")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> Register([FromBody] Input model)
        {
            model.user.result = false;
            ErrorModel error = new ErrorModel();
            if (!ModelState.IsValid)
            {
                error.errMsg = "Errore di validazione";
                return BadRequest(error);
            }
            try
            {
                var user = _userManager.FindByNameAsync(model.user.userName).Result;
                if (user != null)
                {
                    _logger.LogError("Impossibile creare utente con Username " + model.user.userName + " . utente già esistente");
                    error.errMsg = "Impossibile creare utente con Username " + model.user.userName + " . utente già esistente";
                    return BadRequest(error);
                }
                var hasher = new PasswordHasher<IdentityUser>();
                var passwordhash = hasher.HashPassword(null, model.user.password);
                user = new ApplicationUser()
                {
                    Password = model.user.password,
                    CodiceFiscale = model.user.codicefiscale,
                    EmailConfirmed = false,
                    PasswordHash = passwordhash,
                    AccessFailedCount = 0,
                    Cognome = model.user.cognome,
                    Nome = model.user.nome,
                    NormalizedEmail = model.user.email.ToLower(),
                    Email = model.user.email.ToLower(),
                    NormalizedUserName = model.user.userName.ToLower(),
                    UserName = model.user.userName,
                    Sesso = model.user.sesso,

                };
                List<ApplicationUserRole> applicationUserRoles = new List<ApplicationUserRole>();
                //     user.UserRoles = applicationUserRoles;
                CancellationToken cancellationToken = new CancellationToken();
                var result = await _utentiService.CreateAsync(user, cancellationToken);
                if (result.Succeeded)
                {
                    var r = _utentiService.AddToRoleAsync(user, "user", cancellationToken);
                    string body = _mailConfig.Value.BodyCreazioneUtente + "<br /> UserName: " + user.UserName + " <br /> Password: " + model.user.password;
                    var t = _emailSender.SendEmailAsync(user.Email, _mailConfig.Value.SoggettoCreazioneUtente, body);
                    model.user.result = true;
                }
                else
                {
                    _logger.LogError("Errore nella creazione utente");
                    error.errMsg = "Errore nella creazione utente";
                    return BadRequest(error);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }
            model.user.url = "/account/register";
            return Ok(model);
        }

        [Authorize]
        [HttpPost("change")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ChangePassword([FromBody] Input model)
        {
            model.change.Result = false;
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
                user.Password = model.change.Password;
                CancellationToken cancellationToken = new CancellationToken();
                var result = await _utentiService.UpdateAsync(user, cancellationToken);
                if (result.Succeeded)
                {
                    model.change.Url = "/account/ChangePasswordConfirm";
                    model.change.Result = true;
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



        [Authorize]
        [HttpGet("usersfilters")]
        public async Task<IActionResult> GetUsersFilters(string page, string filtro, [FromQuery(Name = "fitriarray[]")] string[] filtriarray)
        {

            ErrorModel error = new ErrorModel();
            List<UserModel> usersmodel = new List<UserModel>();
            try
            {
                int take = int.Parse(_pagingConfig.Value.perPage);
                int skip = take * (int.Parse(page) - 1);
                List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
                if (filtriarray.Length > 0)
                {
                    foreach (string t in filtriarray)
                    {
                        switch (t.ToLower())
                        {
                            case "username":
                                applicationUsers.AddRange(_utentiService.GetUsersByUsernameLike(filtro, take, skip));
                                break;
                            case "email":
                                applicationUsers.AddRange(_utentiService.GetUsersByMailLike(filtro, take, skip));
                                break;
                            case "cognome":
                                applicationUsers.AddRange(_utentiService.GetUsersByCognomeLike(filtro, take, skip));
                                break;
                            default:
                                break;
                        }
                    }
                }
                else
                {
                    applicationUsers.AddRange(_utentiService.GetUsersByUsernameLike(filtro, take, skip));
                    applicationUsers.AddRange(_utentiService.GetUsersByMailLike(filtro, take, skip));
                    applicationUsers.AddRange(_utentiService.GetUsersByCognomeLike(filtro, take, skip));
                }

                if (applicationUsers == null)
                {
                    return Ok(usersmodel);
                }           
                usersmodel = applicationUsers.Distinct().Select(x => new UserModel
                {
                    id = x.Id.ToString(),
                    cognome = x.Cognome,
                    email = x.Email,
                    isActive = !x.LockoutEnabled,
                    nome = x.Nome,
                    userName = x.UserName,
                    role = string.Join(",", x.Roles.ToList())
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }
            return await System.Threading.Tasks.Task.FromResult(Ok(usersmodel));
        }

        [Authorize]
        [HttpGet("users")]
        public async Task<IActionResult> GetUsers(string page,string ordinaPer,[FromQuery] bool ordinaDesc, string filter, [FromQuery(Name = "fitriarray[]")] string[] filtriarray)
        {

            ErrorModel error = new ErrorModel();
            List<UserModel> usersmodel = new List<UserModel>();
            try
            {
                int take = int.Parse(_pagingConfig.Value.perPage);
                int skip = take * (int.Parse(page) - 1);
                List<ApplicationUser> applicationUsers = new List<ApplicationUser>();
                applicationUsers = _utentiService.GetUsersSortingBy(take, skip,ordinaPer,ordinaDesc,filter,filtriarray);               
                if (applicationUsers == null)
                {
                    return Ok(usersmodel);
                }
                usersmodel = applicationUsers.Select(x => new UserModel
                {
                    id = x.Id.ToString(),
                    cognome = x.Cognome,
                    email = x.Email,
                    isActive = !x.LockoutEnabled,
                    nome = x.Nome,
                    userName = x.UserName,
                    role = string.Join(",", x.UserRoles.SelectMany(x => x.Role.Name).ToList())
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }
            return await System.Threading.Tasks.Task.FromResult(Ok(usersmodel));
        }

        [Authorize]
        [HttpGet("pagination")]
        public async Task<IActionResult> GetPagination(string type, string page, string ordinaPer, bool ordinaDesc, string filter, [FromQuery(Name = "fitriarray[]")] string[] filtriarray)
        {
            PaginationModel model = new PaginationModel();
            switch (type)
            {
                case "users":
                    model = GetBasePaginationUser();
                    model.currentPage = page;
                    if (!string.IsNullOrEmpty(ordinaPer))
                    { model.sortBy = ordinaPer; }
                    if (ordinaDesc == true)
                    { model.sortDesc = ordinaDesc; }
                    if (string.IsNullOrEmpty(filter))
                    { model.totalRows = _utentiService.GetUsersCount().ToString(); }
                    else { _utentiService.GetUsersCountLike(filter, filtriarray).ToString(); }
                    break;
            }

            return await System.Threading.Tasks.Task.FromResult(Ok(model));
        }


        [Authorize]
        [HttpGet("paginationlike")]
        public async Task<IActionResult> GetPaginationLike(string type, string page, string filtro, [FromQuery(Name = "fitriarray[]")] string[] filtriarray)
        {
            PaginationModel model = new PaginationModel();
            switch (type)
            {
                case "users":
                    model = GetBasePaginationUser();
                    model.currentPage = page;
                    model.totalRows = _utentiService.GetUsersCountLike(filtro,filtriarray).ToString();
                    break;
            }

            return await System.Threading.Tasks.Task.FromResult(Ok(model));
        }


        private PaginationModel GetBasePaginationUser()
        {
            PaginationModel model = new PaginationModel();
            List<PaginationModel.Field> fields = new List<PaginationModel.Field>();
            PaginationModel.Field field = new PaginationModel.Field();
            field.key = "userName";
            field.label = "UserName";
            field.sortable = true;
            field.sortDirection = "desc";
            fields.Add(field);
            PaginationModel.Field field1 = new PaginationModel.Field();
            field1.key = "email";
            field1.label = "Email";
            field1.sortable = true;
            field1.sortDirection = "desc";
            field1.cssclass = "text-center";
            fields.Add(field1);
            PaginationModel.Field field2 = new PaginationModel.Field();
            field2.key = "isActive";
            field2.label = "Attivo";
            field2.sortable = true;
            field2.sortByFormatted = true;
            field2.filterByFormatted = true;
            field2.cssclass = "text-center";         
            field2.formatter = "activeUser";
            fields.Add(field2);
            PaginationModel.Field field3 = new PaginationModel.Field();
            field3.key = "detailsuser";
            field3.label = "";
            PaginationModel.Field field4 = new PaginationModel.Field();
            field4.key = "deleteuser";
            field4.label = "";
            PaginationModel.Field field5 = new PaginationModel.Field();
            field5.key = "disableuser";
            field5.label = "";
            PaginationModel.Field field6 = new PaginationModel.Field();
            field6.key = "resetpassword";
            field6.label = "";
            fields.Add(field3);
            fields.Add(field4);
            fields.Add(field5);
            fields.Add(field6);
            model.perPage = _pagingConfig.Value.perPage;
            List<int> options = new List<int> { 5, 10, 15 };
            model.pageOptions = options.ToArray();
            model.sortBy = "userName";
            model.sortDesc = false;
            model.sortDirection = "asc";
            model.filter = "";
            List<PaginationModel.Options> f = new List<PaginationModel.Options>();
            PaginationModel.Options opt = new PaginationModel.Options
            {
                item = "userName",
                name = "UserName"
            };
            PaginationModel.Options opt1 = new PaginationModel.Options
            {
                item = "email",
                name = "Email"
            };
            PaginationModel.Options opt2 = new PaginationModel.Options
            {
                item = "cognome",
                name = "Cognome"
            };
            f.Add(opt);
            f.Add(opt1);
            model.filterOn = f.ToArray();
            model.infoMal = new PaginationModel.InfoModale { content = "", id = "info-modal", title = "" };
            model.fields = fields.ToArray();
            return model;
        }

        [Authorize]
        [HttpPost("userssorting")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> GetUserSorting([FromBody] SortingModel model)
        {           
            ErrorModel error = new ErrorModel();
            List<UserModel> usersmodel = new List<UserModel>();
            string[] filtriarray = new string[0];
            if (!ModelState.IsValid)
            {
                error.errMsg = "Errore di validazione";
                return BadRequest(error);
            }
            try
            {

                int take = int.Parse(_pagingConfig.Value.perPage);
                int skip = take * (int.Parse(model.currentPage.ToString()) - 1);
                var users = _utentiService.GetUsersSortingBy(take, skip,model.sortBy, model.sortDesc, model.filter,filtriarray);                
                if (users == null)
                {
                    return Ok(usersmodel);
                }
                usersmodel = users.Select(x => new UserModel
                {
                    id = x.Id.ToString(),
                    cognome = x.Cognome,
                    email = x.Email,
                    isActive = !x.LockoutEnabled,
                    nome = x.Nome,
                    userName = x.UserName,
                    role = string.Join(",", x.Roles.ToList())
                }).ToList();

            }
            catch (Exception ex)
            {
                _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                return BadRequest(error);
            }
            return await System.Threading.Tasks.Task.FromResult(Ok(usersmodel));
        }


        [Authorize]
        [HttpPost("deleteuser")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DeleteUser([FromBody] UserModel user)
        {            
            ErrorModel error = new ErrorModel();
            if (!ModelState.IsValid)
            {
                error.errMsg = "Errore di validazione";
                return BadRequest(error);
            }
            try
            {
                var usera = _userManager.FindByIdAsync(user.id).Result;
                if (usera == null)
                {
                    _logger.LogError("Impossibile aggiornare utente con ID " + user.id);
                    error.errMsg = "Impossibile aggiornare utente con UserName: " + user.userName;
                    return BadRequest(error);
                }               
                CancellationToken cancellationToken = new CancellationToken();
                var result = await _utentiService.DeleteAsync(usera, cancellationToken);
                if (result.Succeeded)
                {
                    Ok();
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
            return Ok();
        }

        [Authorize]
        [HttpPost("disableuser")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> DisableUser([FromBody] UserModel user)
        {            
            ErrorModel error = new ErrorModel();
            if (!ModelState.IsValid)
            {
                error.errMsg = "Errore di validazione";
                return BadRequest(error);
            }
            try
            {
                var usera = _userManager.FindByIdAsync(user.id).Result;
                if (usera == null)
                {
                    _logger.LogError("Impossibile aggiornare utente con ID " + user.id);
                    error.errMsg = "Impossibile aggiornare utente con UserName: " + user.userName;
                    return BadRequest(error);
                }
                usera.LockoutEnabled = true;
                CancellationToken cancellationToken = new CancellationToken();
                var result = await _utentiService.UpdateAsync(usera, cancellationToken);
                if (result.Succeeded)
                {
                    Ok();
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
            return Ok();
        }

        [Authorize]
        [HttpPost("resetpassword")]
        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> ResetPassoword([FromBody] UserModel user)
        {
            ErrorModel error = new ErrorModel();
            if (!ModelState.IsValid)
            {
                error.errMsg = "Errore di validazione";
                return BadRequest(error);
            }
            try
            {
                var usera = _userManager.FindByIdAsync(user.id).Result;
                if (usera == null)
                {
                    _logger.LogError("Impossibile aggiornare utente con ID '{model.confirm.Id}'.");
                    error.errMsg = "Impossibile aggiornare utente con username:" + user.userName;
                    return BadRequest(error);
                }
                usera.Password =  GeneratePassword.GetOne(_identityOptions.Value);             
                usera.EmailConfirmed = false;
                CancellationToken cancellationToken = new CancellationToken();
                var result = await _utentiService.UpdateAsync(usera, cancellationToken);     
                if (result.Succeeded)
                {
                    string body = _mailConfig.Value.BodyResetPassword + "<br /> UserName: " + usera.UserName + " <br /> Password: " + usera.Password;
                    var t = _emailSender.SendEmailAsync(usera.Email, _mailConfig.Value.SogggettoResetPassword, body);
                    Ok();
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
            return Ok();
        }

    }
}