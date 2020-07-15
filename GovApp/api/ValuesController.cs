using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Gov.Core.Entity;
using Gov.Core.Entity.Elezioni;
using Gov.Core.Entity.Presentation;
using Gov.Core.Identity;
using Gov.Structure.Config;
using Gov.Structure.Contracts;
using Gov.Structure.Contracts.Elezioni;
using Gov.Structure.Contracts.Helpers;
using GovApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GovApp.api
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;

        private readonly IPaginaService _paginaService;
        private readonly IContenutoService _contenutoService;
        private readonly IVoceMenuService _voceMenuService;
        private readonly IRoleStore<ApplicationRole> _roleService;
        private readonly ISezioneService _sezioneService;
        private readonly ElezioneConfig _elezioneConfig;
        private readonly IBusinessRules _businessRules;

        public ValuesController(ILogger<ValuesController> logger, IPaginaService paginaService, IVoceMenuService voceMenuService, IContenutoService contenutoService, IRoleStore<ApplicationRole> roleService, ISezioneService sezioneService, ElezioneConfig elezioneConfig, IBusinessRules businessRules)
        {
            _logger = logger;
            _paginaService = paginaService;
            _voceMenuService = voceMenuService;
            _contenutoService = contenutoService;
            _roleService = roleService;
            _sezioneService = sezioneService;
            _elezioneConfig = elezioneConfig;
            _businessRules = businessRules;
        }

        [HttpGet("/Values/content")]
        public IActionResult GetValues([FromQuery] string type)
        {
            Dictionary<String, String> errors = null;
            List<ContenutoModel> model = new List<ContenutoModel>();
            try
            {
                List<Pagina> paginas = _paginaService.GetByCodice(type);
                if (paginas == null)
                {
                    return Ok();
                }
                else
                {
                    int id = 0;
                    foreach (Pagina p in paginas)
                    {
                        ContenutoModel contenuto = new ContenutoModel();
                        List<Contenuto> contenutos = _contenutoService.GetByPaginaId(p.Id);
                        if (contenutos.Count > 0)
                        {
                            foreach (Contenuto c in contenutos)
                            {
                                contenuto.Id = "collapse" + id.ToString();
                                switch (c.Tipo.ToLower())
                                {
                                    case "icona":
                                        contenuto.ContenutoIcon = c.ContentuoCard;
                                        break;
                                    case "testo":
                                        contenuto.ContenutoTesto = c.ContentuoCard;
                                        break;
                                    case "link":
                                        contenuto.ContenutoLink = c.ContentuoCard;
                                        break;
                                    case "header":
                                        contenuto.ContenutoHeader = c.ContentuoCard;
                                        break;

                                }
                            }
                            model.Add(contenuto);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errors = new Dictionary<string, string>();
                errors.Add("Errore grave", ex.Message);
                _logger.LogError(errors.First().Value);
                return BadRequest(errors.First().Value);
            }
            return Ok(model);
        }

        [Authorize]
        [HttpGet("/Values/menu")]
        public IActionResult GetMenu()
        {
            Dictionary<String, String> errors = null;
            List<VociModel> model = new List<VociModel>();
            string role = this.User?.FindFirstValue(ClaimTypes.Role);
            try
            {

                CancellationToken cancellationToken = new CancellationToken();
                ApplicationRole applicationRole = _roleService.FindByNameAsync(role, cancellationToken).Result;
                List<VoceMenu> l = applicationRole.VociMenu.ToList();
                foreach (VoceMenu v in l)
                {
                    VociModel voce = new VociModel();
                    voce.icon = v.Icona;
                    voce.active = v.Active;
                    voce.link = v.Link;
                    voce.descrizione = v.Voce;
                    model.Add(voce);
                }
            }
            catch (Exception ex)
            {
                errors = new Dictionary<string, string>();
                errors.Add("Errore grave", ex.Message);
                _logger.LogError(errors.First().Value);
                return BadRequest(errors.First().Value);
            }
            return Ok(model);
        }

        [Authorize]
        [HttpGet("/Values/carousel")]
        public IActionResult GetCarousel([FromQuery] string type)
        {
            Dictionary<String, String> errors = null;
            List<ContenutoModel> model = new List<ContenutoModel>();
            try
            {
                List<Pagina> paginas = _paginaService.GetByCodice(type);
                if (paginas == null)
                {
                    return Ok();
                }
                else
                {

                    foreach (Pagina p in paginas)
                    {
                        ContenutoModel contenuto = new ContenutoModel();
                        List<Contenuto> contenutos = _contenutoService.GetByPaginaId(p.Id).OrderBy(z => z.Id).ToList();
                        if (contenutos.Count > 0)
                        {
                            foreach (Contenuto c in contenutos)
                            {
                                switch (c.Tipo.ToLower())
                                {
                                    case "titolo":
                                        contenuto.ContenutoTitolo = c.ContentuoCard;
                                        model.Add(contenuto);
                                        contenuto = new ContenutoModel();
                                        break;
                                    case "image":
                                        contenuto.ContenutoImmagine = c.ContentuoCard;
                                        break;
                                }
                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                errors = new Dictionary<string, string>();
                errors.Add("Errore grave", ex.Message);
                _logger.LogError(errors.First().Value);
                return BadRequest(errors.First().Value);
            }
            return Ok(model);
        }

        [Authorize]
        [HttpPost("/Values/ResearchSezione")]
        [IgnoreAntiforgeryToken]
        public IActionResult ResearchSezione([FromBody] ResearchSezione model)
        {
            ErrorModel error = new ErrorModel();
            Dictionary<String, String> errors = null;
            String msg = "";
            SezioneModel sezioneModel = new SezioneModel();
            if (ModelState.IsValid)
            {
                try
                {
                    int t = int.Parse(_elezioneConfig.tipoElezioneId);
                    if (model.Tipo != "S")
                    {
                        msg = _businessRules.IsInsertable(model.Sezione, model.Tipo, model.Cabina, t);
                    }
                    if (string.IsNullOrEmpty(msg))
                    {
                        //  iscritti = _iscrittiService.findByTipoelezioneIdAndSezioneNumerosezione(tipoelezioneid, model.Sezione);
                        Sezioni sezione = _sezioneService.findByNumerosezioneAndTipoelezioneId(model.Sezione, t);
                        if (sezione.Cabina == model.Cabina)
                        {
                            sezioneModel.Iscritti = new IscrittiModel
                            {
                                iscrittimaschi = sezione.Iscritti.Iscrittimaschigen.ToString(),
                                iscrittifemmine = sezione.Iscritti.Iscrittifemminegen.ToString(),
                                iscrittitotali = sezione.Iscritti.Iscrittitotaligen.ToString()
                            };
                            sezioneModel.NumeroSezione = model.Sezione.ToString();
                            sezioneModel.Municipio = sezione.Iscritti.Municipio.ToString();
                            sezioneModel.DescrizioneSezione = sezione.IdtiposezioneNavigation.Descrizione;
                            sezioneModel.DescrizioneElezione = sezione.IdtipoelezioneNavigation.Descrizione;

                        }
                        else
                        {
                            errors = new Dictionary<string, string>();
                            errors.Add("Errore grave", "Sezione cabina non congruenti");
                            _logger.LogError(errors.First().Value);
                            return BadRequest(errors.First().Value);
                        }
                    }
                    else
                    {
                        errors = new Dictionary<string, string>();
                        errors.Add("Errore grave", msg);
                        _logger.LogError(errors.First().Value);
                        return BadRequest(errors.First().Value);
                    }
                  
                }
                catch (Exception ex)
                {
                    _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                    error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                    return BadRequest(error);
                }
            }
            return Ok(model);
        }

    }
}