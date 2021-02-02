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
using Gov.Structure.Identity;
using GovApp.Helpers;
using GovApp.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GovApp.api
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private readonly ILogger<ValuesController> _logger;
        private readonly UserStore _utentiService;
        private readonly IPaginaService _paginaService;
        private readonly IContenutoService _contenutoService;
        private readonly IVoceMenuService _voceMenuService;
        private readonly IRoleStore<ApplicationRole> _roleService;
        private readonly ISezioneService _sezioneService;
        private readonly IOptions<ElezioneConfig> _elezioneConfig;
        private readonly IBusinessRules _businessRules;
        private readonly IAbilitazioniService _abilitazioniService;
        private readonly IAffluenzaService _affluenzaService;
        private readonly IIscrittiService _iscrittiService;
        private readonly ITipoElezioneService _tipoElezioneService;
        private readonly IListaService _listaService;
        private readonly IMunicipioService _municipioService;
        private readonly IVotiSindacoService _votiSindacoService;
        private readonly IVotiPreferenzeService _votiPreferenzeService;

        public ValuesController(ILogger<ValuesController> logger, IPaginaService paginaService, IVoceMenuService voceMenuService, IContenutoService contenutoService, IRoleStore<ApplicationRole> roleService, ISezioneService sezioneService, IOptions<ElezioneConfig> elezioneConfig, IBusinessRules businessRules, IAbilitazioniService abilitazioniService, IAffluenzaService affluenzaService, ITipoElezioneService tipoElezioneService, IIscrittiService iscrittiService, UserStore utentiService, IMunicipioService municipioService, IListaService listaService, IVotiSindacoService votiSindacoService, IVotiPreferenzeService votiPreferenzeService)
        {
            _logger = logger;
            _paginaService = paginaService;
            _voceMenuService = voceMenuService;
            _contenutoService = contenutoService;
            _roleService = roleService;
            _sezioneService = sezioneService;
            _elezioneConfig = elezioneConfig;
            _businessRules = businessRules;
            _abilitazioniService = abilitazioniService;
            _affluenzaService = affluenzaService;
            _tipoElezioneService = tipoElezioneService;
            _iscrittiService = iscrittiService;
            _utentiService = utentiService;
            _listaService = listaService;
            _municipioService = municipioService;
            _votiSindacoService = votiSindacoService;
            _votiPreferenzeService = votiPreferenzeService;
        }


        public class Input
        {
           public Research research { get; set; }
           public AndamentoModel anda { get; set; }

            public AffluenzaModel affluenza { get; set; }
           
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

        [AllowAnonymous]
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

       //  [Authorize]
        [HttpPost("/Values/ResearchSezione")]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public IActionResult ResearchSezione([FromBody] Input model)
        {
            ErrorModel error = new ErrorModel();          
            String msg = "";
            SezioneModel sezioneModel = new SezioneModel();
            Research research = model.research;
            if (ModelState.IsValid)
            {
                try
                {
                    int t = int.Parse(_elezioneConfig.Value.tipoelezioneid);
                    if (!string.IsNullOrEmpty(research.tipo))
                    {
                        msg = _businessRules.IsInsertable(int.Parse(research.sezione),research.tipo, int.Parse(research.cabina), t);
                    }
                    if (string.IsNullOrEmpty(msg))
                    {
                        //  iscritti = _iscrittiService.findByTipoelezioneIdAndSezioneNumerosezione(tipoelezioneid, model.Sezione);
                        Sezioni sezione = _sezioneService.findByNumerosezioneAndTipoelezioneId(int.Parse(research.sezione), t);
                        if (sezione.Cabina == int.Parse(research.cabina))
                        {
                            sezioneModel.Iscritti = new IscrittiModel
                            {
                                iscrittimaschi = sezione.Iscritti.Iscrittimaschigen.ToString(),
                                iscrittifemmine = sezione.Iscritti.Iscrittifemminegen.ToString(),
                                iscrittitotali = sezione.Iscritti.Iscrittitotaligen.ToString()
                            };
                            sezioneModel.NumeroSezione = research.sezione.ToString();
                            sezioneModel.Sezione = int.Parse(research.sezione);
                            sezioneModel.Cabina = int.Parse(research.cabina);
                            sezioneModel.Municipio = sezione.Iscritti.Municipio.ToString();
                            sezioneModel.DescrizioneSezione = sezione.IdtiposezioneNavigation.Descrizione;
                            sezioneModel.DescrizioneElezione = sezione.IdtipoelezioneNavigation.Descrizione;
                            sezioneModel.UbicazionePlesso= sezione.IdplessoNavigation.Ubicazione;
                            sezioneModel.DescrizionePlesso = sezione.IdplessoNavigation.Descrizione;
                            sezioneModel.Tipo = research.tipo;                           
                            if(string.IsNullOrEmpty(research.tipo) && sezione.UsersSezioni.First() != null)
                            {
                                sezioneModel.UserId = sezione.UsersSezioni.First().UserId;
                                CancellationToken token = new CancellationToken();
                                ApplicationUser user = _utentiService.FindByIdAsync(sezione.UsersSezioni.First().UserId.ToString(),token).Result;
                                sezioneModel.UserName = user.UserName; 
                            }
                        }
                        else
                        {
                           
                            error.errMsg= "Errore grave Sezione cabina non congruenti";                          
                            return BadRequest(error);
                        }
                    }
                    else
                    {
                        error.errMsg = msg;
                        return BadRequest(error);                      
                   
                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                    error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                    return BadRequest(error);
                }
            }
            return Ok(sezioneModel);
        }

        [HttpPost("/Values/StatusSezione")]
        [IgnoreAntiforgeryToken]
        [AllowAnonymous]
        public IActionResult StatusSezione([FromBody] Input model)
        {
            ErrorModel error = new ErrorModel();
            String msg = "";
            SezioneModel sezioneModel = new SezioneModel();
            Research research = model.research;
            if (ModelState.IsValid)
            {
                try
                {
                    int t = int.Parse(_elezioneConfig.Value.tipoelezioneid);
                    if (!string.IsNullOrEmpty(research.tipo))
                    {
                        msg = _businessRules.IsInsertable(int.Parse(research.sezione), research.tipo, int.Parse(research.cabina), t);
                    }
                    if (string.IsNullOrEmpty(msg))
                    {
                        //  iscritti = _iscrittiService.findByTipoelezioneIdAndSezioneNumerosezione(tipoelezioneid, model.Sezione);
                        Sezioni sezione = _sezioneService.findByNumerosezioneAndTipoelezioneId(int.Parse(research.sezione), t);
                        if (sezione.Cabina == int.Parse(research.cabina))
                        {
                            sezioneModel.Iscritti = new IscrittiModel
                            {
                                iscrittimaschi = sezione.Iscritti.Iscrittimaschigen.ToString(),
                                iscrittifemmine = sezione.Iscritti.Iscrittifemminegen.ToString(),
                                iscrittitotali = sezione.Iscritti.Iscrittitotaligen.ToString()
                            };
                            sezioneModel.NumeroSezione = research.sezione.ToString();
                            sezioneModel.Sezione = int.Parse(research.sezione);
                            sezioneModel.Cabina = int.Parse(research.cabina);
                            sezioneModel.Municipio = sezione.Iscritti.Municipio.ToString();
                            sezioneModel.DescrizioneSezione = sezione.IdtiposezioneNavigation.Descrizione;
                            sezioneModel.DescrizioneElezione = sezione.IdtipoelezioneNavigation.Descrizione;
                            sezioneModel.UbicazionePlesso = sezione.IdplessoNavigation.Ubicazione;
                            sezioneModel.DescrizionePlesso = sezione.IdplessoNavigation.Descrizione;
                            sezioneModel.Tipo = research.tipo;
                            sezioneModel.UserName = sezione.UsersSezioni != null ? sezione.UsersSezioni.First().User.UserName : "N/A";
                            Affluenze affluenze = _affluenzaService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(research.sezione), t);
                            List<Status> statusSezione = ModelConversion.ConvertStatus(affluenze);
                            switch(t)
                            {
                                case 4:
                                   var votiSindaco = _votiSindacoService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(research.sezione), t);
                                    Status status = new Status
                                    {
                                        id = "VS",
                                        nome = "VOTI SINDACO-LISTE",
                                        pervenuto = votiSindaco.Count > 0 ? true : false,
                                        user = votiSindaco.Count > 0 ? votiSindaco.First().CreatedBy : "N/A",
                                        dataregistrazione = votiSindaco.Count > 0 ? votiSindaco.First().CreatedDate.ToString("dd/MM/yyyy") : "N/A"
                                    };
                                    statusSezione.Add(status);
                                    var votiPreferenze = _votiPreferenzeService.countPervenuteBySezioneAndTipoElezione(int.Parse(research.sezione), t);
                                    Status statusp = new Status
                                    {
                                        id = "VP",
                                        nome = "VOTI PREFERENZE",
                                        pervenuto = votiPreferenze > 0 ? true : false                                      
                                    };
                                    statusSezione.Add(statusp);
                                    break;
                            }
                            sezioneModel.StatusSezione = statusSezione;
                        }
                        else
                        {

                            error.errMsg = "Errore grave Sezione cabina non congruenti";
                            return BadRequest(error);
                        }
                    }
                    else
                    {
                        error.errMsg = msg;
                        return BadRequest(error);

                    }

                }
                catch (Exception ex)
                {
                    _logger.LogError("Eccezione non gestita dettagli: " + ex.Message);
                    error.errMsg = "Eccezione non gestita contattare amministrazione di sistema";
                    return BadRequest(error);
                }
            }
            return Ok(sezioneModel);
        }
               
        [Authorize]
        [HttpGet("/Values/categories")]
        public IActionResult Categories([FromQuery] string categoria)
        {
            ErrorModel error = new ErrorModel();
            List<AbilitazioniModel> model = new List<AbilitazioniModel>();
            try
            {
              List<FaseElezione> fases =  _abilitazioniService.findByCategoria(categoria, int.Parse(_elezioneConfig.Value.tipoelezioneid),0,100);
                model = fases.Where(x=>x.Abilitata == 1).Distinct().Select(x => new AbilitazioniModel
                {
                    codice = x.Codice,
                    descrizione = x.Descrizione
                }).ToList();
            }
            catch (Exception ex)
            {
               
                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok(model);
        }

        [Authorize]
        [HttpGet("/Values/liste")]
        public IActionResult Liste()
        {
            ErrorModel error = new ErrorModel();
            List<AbilitazioniModel> model = new List<AbilitazioniModel>();
            model.Add(new AbilitazioniModel("", "Selezionare un'opzione"));
            try
            {
                var liste = _listaService.findAllByTipoelezioneId(int.Parse(_elezioneConfig.Value.tipoelezioneid));
                model.AddRange(liste.Select(x => new AbilitazioniModel
                {
                    codice = x.Id.ToString(),
                    descrizione = x.Denominazione
                }).ToList());
            }
            catch (Exception ex)
            {

                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok(model);
        }


        [Authorize]
        [HttpGet("/Values/municipi")]
        public IActionResult Municipi()
        {
            ErrorModel error = new ErrorModel();
            List<AbilitazioniModel> model = new List<AbilitazioniModel>();
            model.Add(new AbilitazioniModel("", "Selezionare un'opzione"));
            try
            {
                var municipi = _municipioService.GetAll();
                model.AddRange(municipi.Select(x => new AbilitazioniModel
                {
                    codice = x.Municipio.ToString(),
                    descrizione = x.Descrizione
                }).ToList());
            }
            catch (Exception ex)
            {

                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok(model);
        }
      

    }
}