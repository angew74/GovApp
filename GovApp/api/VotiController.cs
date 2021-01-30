using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace GovApp.api
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class VotiController : ControllerBase
    {
        private readonly ILogger<VotiController> _logger;
        private readonly UserStore _utentiService;
        private readonly IPaginaService _paginaService;
        private readonly IVotiGeneraliService _votiGeneraliService;
        private readonly IListaService _listaService;
        private readonly IRoleStore<ApplicationRole> _roleService;
        private readonly ISezioneService _sezioneService;
        private readonly IOptions<ElezioneConfig> _elezioneConfig;
        private readonly IBusinessRules _businessRules;
        private readonly IAbilitazioniService _abilitazioniService;
        private readonly IAffluenzaService _affluenzaService;
        private readonly IIscrittiService _iscrittiService;
        private readonly ISindacoService _sindacoService;
        private readonly IVotiListaService _votiListaService;
        private readonly IVotiSindacoService _votiSindacoService;
        private readonly IVotiLoader _votiLoader;

        public VotiController(ILogger<VotiController> logger, IPaginaService paginaService, IListaService listaService, IVotiGeneraliService votiGeneraliService, IRoleStore<ApplicationRole> roleService, ISezioneService sezioneService, IOptions<ElezioneConfig> elezioneConfig, IBusinessRules businessRules, IAbilitazioniService abilitazioniService, IAffluenzaService affluenzaService, ISindacoService sindacoService, IIscrittiService iscrittiService, UserStore utentiService, IVotiSindacoService votiSindacoService, IVotiListaService votiListaService, IVotiLoader votiLoader)
        {
            _logger = logger;
            _paginaService = paginaService;
            _listaService = listaService;
            _votiGeneraliService = votiGeneraliService;
            _roleService = roleService;
            _sezioneService = sezioneService;
            _elezioneConfig = elezioneConfig;
            _businessRules = businessRules;
            _abilitazioniService = abilitazioniService;
            _affluenzaService = affluenzaService;
            _sindacoService = sindacoService;
            _iscrittiService = iscrittiService;
            _utentiService = utentiService;
            _votiListaService = votiListaService;
            _votiSindacoService = votiSindacoService;
            _votiLoader = votiLoader;
        }

        public class Input
        {
            public Research research { get; set; }
            public VotiModel voti { get; set; }
            public DatiModel ricalcolo { get; set; }

        }

        [HttpPost("/voti/interrogazione")]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult Interrogazione([FromBody] Input input)
        {
            MunicipioModel voti = new MunicipioModel();
            ErrorModel error = new ErrorModel();
            Research model = input.research;
            try
            {
                switch (input.research.tipo)
                {
                    case "L":
                        switch (input.research.tipoInterrogazione)
                        {
                            case "1":
                                voti = getVotiLista(input.research);
                                break;
                            case "2":
                                voti = getVotiListaMunicipio(input.research);
                                break;
                            case "3":
                                voti = getVotiListaSezione(input.research);
                                break;
                        }
                        break;
                    case "S":
                        break;
                    case "R":
                        break;
                }
            }
            catch (Exception ex)
            {
                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok(voti);
        }

        [HttpGet("/voti/ricalcolo")]
        [Authorize]
        public IActionResult Ricalcolo(string tipo, string municipio)
        {
            MunicipioModel voti = new MunicipioModel();
            ErrorModel error = new ErrorModel();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            try
            {
                switch (tipo)
                {
                    case "L":
                        switch (municipio)
                        {
                            case "99":
                                var ricalcoloComune = _votiListaService.countLista(tipoelezioneid);
                                var ricalcoloGenerale = _votiGeneraliService.countGenerale(tipoelezioneid);
                                if(ricalcoloGenerale == null) { return Ok(voti); }
                                voti = _votiLoader.ConvertToJsonListeRicalcolo(ricalcoloComune, ricalcoloGenerale);
                                break;
                            default:
                                var ricalcoloMunicipio = _votiListaService.countListaByMunicipio(tipoelezioneid, int.Parse(municipio));
                                var ricalcoloMunGenerale = _votiGeneraliService.countGeneraleOverMunicipio(tipoelezioneid,int.Parse(municipio));
                                if(ricalcoloMunGenerale == null) { return Ok(voti); }
                                voti = _votiLoader.ConvertToJsonListeRicalcolo(ricalcoloMunicipio, ricalcoloMunGenerale);
                                break;
                        }
                        break;
                    case "S":
                        switch (municipio)
                        {
                            case "99":
                                var ricalcoloSindacoComune = _votiSindacoService.countSindaco(tipoelezioneid);
                                var ricalcoloSindacoGenerale = _votiGeneraliService.countGenerale(tipoelezioneid);
                                if (ricalcoloSindacoGenerale == null) { return Ok(voti); }
                                voti = _votiLoader.ConvertToJsonSindacoRicalcolo(ricalcoloSindacoComune, ricalcoloSindacoGenerale);
                                break;
                            default:
                                var ricalcoloSindacoMunicipio = _votiSindacoService.countSindacoByMunicipio(tipoelezioneid, int.Parse(municipio));
                                var ricalcoloSindacoMunGenerale = _votiGeneraliService.countGeneraleOverMunicipio(tipoelezioneid, int.Parse(municipio));
                                if (ricalcoloSindacoMunGenerale == null) { return Ok(voti); }
                                voti = _votiLoader.ConvertToJsonSindacoRicalcolo(ricalcoloSindacoMunicipio, ricalcoloSindacoMunGenerale);
                                break;
                        }
                        break;
                    case "P":
                        break;

                }
            }
            catch (Exception ex)
            {
                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok(voti);
        }
        private MunicipioModel getVotiListaMunicipio(Research research)
        {
            MunicipioModel model = new MunicipioModel();
            List<VotiLista> votis = new List<VotiLista>();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            //  votis = _votiListaService.findByMunicipioAndTipoelezioneId(int.Parse(research.municipio), tipoelezioneid);
            //  var votiGeneralis = _votiGeneraliService.findByMunicipioAndTipoelezioneId(int.Parse(research.municipio), tipoelezioneid);           
            //  if(votiGeneralis == null || votiGeneralis.Count == 0) { return model; }
            //  var ricalcolo = _votiListaService.countVotantiPervenuteByMunicipio(tipoelezioneid, int.Parse(research.municipio));
            //  model = _votiLoader.ConvertToJsonListaMunicipio(votis, votiGeneralis.FirstOrDefault(), ricalcolo.IscrittiPervenute);
            return model;
        }

        private MunicipioModel getVotiListaSezione(Research research)
        {
            List<VotiLista> votis = new List<VotiLista>();
            MunicipioModel model = new MunicipioModel();
            VotiGenerali votiGenerali = new VotiGenerali();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            votis = _votiListaService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(research.sezione), tipoelezioneid);
            votiGenerali = _votiGeneraliService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(research.sezione), tipoelezioneid);
            if (votiGenerali == null) { return model; }
            model = _votiLoader.ConvertToJsonLista(votis, votiGenerali);
            return model;
        }

        private MunicipioModel getVotiLista(Research research)
        {
            List<VotiLista> votis = new List<VotiLista>();
            MunicipioModel model = new MunicipioModel();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            votis = _votiListaService.findByListaIdAndTipoelezioneId(int.Parse(research.idlista), tipoelezioneid);
            // var ricalcolo = _votiListaService.countVotantiPervenuteByMunicipio(tipoelezioneid, int.Parse(research.municipio));            
            // if (votiGenerali == null) { return model; }
            // model = _votiLoader.ConvertToJsonListaMunicipio(votis, votiGenerali.FirstOrDefault(), iscritti);
            return model;
        }


        [HttpPost("/voti/carica")]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult Carica([FromBody] Input input)
        {
            VotiModel json = new VotiModel();
            ErrorModel error = new ErrorModel();
            Research model = input.research;
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            try
            {
                int s = int.Parse(model.sezione);
                Affluenze afp = _affluenzaService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                if (afp.Affluenza3 != 1)
                {
                    error.errMsg = "ultima affluenza per la sezione numero " + model.sezione + " non inserita";
                    return BadRequest(error);
                }
                var votiSindaco = _votiSindacoService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                if (votiSindaco.Count > 0 && model.tipo == "VL")
                {
                    error.errMsg = "voti già inseriti per la sezione numero " + model.sezione + " utilizzare modifica";
                    return BadRequest(error);
                }
                json.Votanti = afp.Votantitotali3.ToString();
                json.Iscritti = afp.Iscritti.Iscrittitotaligen.ToString();
                json.NumeroSezione = input.research.sezione;
                json.Municipio = _sezioneService.findByNumerosezioneAndTipoelezioneId(int.Parse(input.research.sezione), tipoelezioneid).Municipio.ToString();
                json.Tipo = input.research.tipo;
                if (model.tipo == "VL")
                {
                    var sindaci = _sindacoService.findAllByTipoelezioneId(tipoelezioneid);
                    json.Sindaci = _votiLoader.ConvertToJsonSindaciEmpty(sindaci, input.research.sezione, input.research.tipo);
                    var liste = _listaService.findAllByTipoelezioneId(tipoelezioneid);
                }
                else
                {
                    var votiLista = _votiListaService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                    json.Sindaci = _votiLoader.ConvertToJsonSindaci(votiSindaco, s, input.research.tipo, tipoelezioneid);
                }
            }
            catch (Exception ex)
            {
                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok(json);
        }

        [HttpPost("/voti/lcom")]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult Registra([FromBody] Input input)
        {
            VotiModel json = input.voti;
            ErrorModel error = new ErrorModel();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            try
            {

                int s = int.Parse(json.NumeroSezione);
                String msg = _businessRules.IsInsertable(s, "VL", 0, tipoelezioneid);
                if (string.IsNullOrEmpty(msg))
                {
                    var voti = _votiLoader.prepareVoti(json, tipoelezioneid);
                    _votiSindacoService.CreateRange(voti);
                }
            }
            catch (Exception ex)
            {
                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok();
        }

        [HttpPost("/voti/save")]
        [IgnoreAntiforgeryToken]
        [Authorize]
        public IActionResult Save([FromBody] Input input)
        {
            DatiModel json = input.ricalcolo;
            ErrorModel error = new ErrorModel();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            try
            {
                               
            }
            catch (Exception ex)
            {
                error.errMsg = "Errore grave :" + ex.Message;
                _logger.LogError("Errore grave :" + ex.Message);
                return BadRequest(error);
            }
            return Ok();
        }
    }
}
