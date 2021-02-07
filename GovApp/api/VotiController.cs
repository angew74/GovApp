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
using Gov.Core.Contracts;
using Gov.Core.Contracts.Elezioni;
using Gov.Core.Contracts.Helpers;
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
        private readonly IRicalcoloListaService _ricalcoloListaService;
        private readonly IVotiGeneraliService _votiGeneraliService;
        private readonly IListaService _listaService;
        private readonly IRicalcoloSindacoService _ricalcoloSindacoService;
        private readonly ISezioneService _sezioneService;
        private readonly IOptions<ElezioneConfig> _elezioneConfig;
        private readonly IBusinessRules _businessRules;
        private readonly IVotiSindacoStoricoService _votiSindacoStoricoService;
        private readonly IAffluenzaService _affluenzaService;      
        private readonly ISindacoService _sindacoService;
        private readonly IVotiListaService _votiListaService;
        private readonly IVotiSindacoService _votiSindacoService;
        private readonly IVotiLoader _votiLoader;


        public VotiController(ILogger<VotiController> logger, IRicalcoloListaService ricalcoloListaService, IListaService listaService, IVotiGeneraliService votiGeneraliService, IRicalcoloSindacoService ricalcoloSindacoService, ISezioneService sezioneService, IOptions<ElezioneConfig> elezioneConfig, IBusinessRules businessRules, IVotiSindacoStoricoService votiSindacoStoricoService, IAffluenzaService affluenzaService, ISindacoService sindacoService, UserStore utentiService, IVotiSindacoService votiSindacoService, IVotiListaService votiListaService, IVotiLoader votiLoader)
        {
            _logger = logger;
            _ricalcoloListaService = ricalcoloListaService;
            _listaService = listaService;
            _votiGeneraliService = votiGeneraliService;
            _ricalcoloSindacoService = ricalcoloSindacoService;
            _sezioneService = sezioneService;
            _elezioneConfig = elezioneConfig;
            _businessRules = businessRules;
            _votiSindacoStoricoService = votiSindacoStoricoService;
            _affluenzaService = affluenzaService;
            _sindacoService = sindacoService;            
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
                        switch (input.research.tipoInterrogazione)
                        {
                            case "1":
                                voti = getVotiSindaco(input.research);
                                break;
                            case "2":
                                voti = getVotiSindacoMunicipio(input.research);
                                break;
                            case "3":
                                voti = getVotiSindacoSezione(input.research);
                                break;
                        }
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
                                if (ricalcoloGenerale == null) { return Ok(voti); }
                                voti = _votiLoader.ConvertToJsonListeRicalcolo(ricalcoloComune, ricalcoloGenerale);
                                break;
                            default:
                                var ricalcoloMunicipio = _votiListaService.countListaByMunicipio(tipoelezioneid, int.Parse(municipio));
                                var ricalcoloMunGenerale = _votiGeneraliService.countGeneraleOverMunicipio(tipoelezioneid, int.Parse(municipio));
                                if (ricalcoloMunGenerale == null) { return Ok(voti); }
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
            List<RicalcoloVotiLista> votis = new List<RicalcoloVotiLista>();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            votis = _ricalcoloListaService.findByMunicipio(tipoelezioneid, int.Parse(research.municipio));
            if (votis == null || votis.Count == 0) { return model; }           
            model = _votiLoader.ConvertToJsonListaMunicipio(votis);
            return model;
        }

        private MunicipioModel getVotiSindacoMunicipio(Research research)
        {
            MunicipioModel model = new MunicipioModel();
            List<RicalcoloVotiSindaco> votis = new List<RicalcoloVotiSindaco>();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            votis = _ricalcoloSindacoService.findByMunicipio(tipoelezioneid, int.Parse(research.municipio));
            if (votis == null || votis.Count == 0) { return model; }
            model = _votiLoader.ConvertToJsonSindacoMunicipio(votis);
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
            model = _votiLoader.ConvertToJsonListaSezione(votis, votiGenerali);
            return model;
        }
        private MunicipioModel getVotiSindacoSezione(Research research)
        {
            List<VotiSindaco> votis = new List<VotiSindaco>();
            MunicipioModel model = new MunicipioModel();
            VotiGenerali votiGenerali = new VotiGenerali();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            votis = _votiSindacoService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(research.sezione), tipoelezioneid);
            votiGenerali = _votiGeneraliService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(research.sezione), tipoelezioneid);
            if (votiGenerali == null) { return model; }
            model = _votiLoader.ConvertToJsonSindacoSezione(votis, votiGenerali);
            return model;
        }
        private MunicipioModel getVotiLista(Research research)
        {
            List<RicalcoloVotiLista> votis = new List<RicalcoloVotiLista>();
            MunicipioModel model = new MunicipioModel();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            votis = _ricalcoloListaService.findByLista(tipoelezioneid, int.Parse(research.idlista));
            if (votis == null || votis.Count == 0) { return model; }
            model = _votiLoader.ConvertToJsonLista(votis);
            return model;
        }

        private MunicipioModel getVotiSindaco(Research research)
        {
            List<RicalcoloVotiSindaco> votis = new List<RicalcoloVotiSindaco>();
            MunicipioModel model = new MunicipioModel();
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            votis = _ricalcoloSindacoService.findBySindaco(tipoelezioneid, int.Parse(research.idsindaco));
            if (votis == null || votis.Count == 0) { return model; }
            model = _votiLoader.ConvertToJsonSindaco(votis);
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
            int numerosindaci = int.Parse(_elezioneConfig.Value.numerosindaci);
            try
            {
                int s = int.Parse(model.sezione);
                Affluenze afp = _affluenzaService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                if (afp.Affluenza3 != 1)
                {
                    error.errMsg = "ultima affluenza per la sezione numero " + model.sezione + " non inserita";
                    return BadRequest(error);
                }
                var votiLista = _votiListaService.findBySezioneNumerosezioneAndTipoelezioneId(s, tipoelezioneid);
                if (votiLista.Count > 0 && model.tipo == "VL")
                {
                    error.errMsg = "voti già inseriti per la sezione numero " + model.sezione + " utilizzare modifica";
                    return BadRequest(error);
                }
                if(votiLista.Count == 0 && model.tipo == "RVL")
                {
                    error.errMsg = "voti NON inseriti per la sezione numero " + model.sezione + " utilizzare inserimento";
                    return BadRequest(error);
                }               
                if (model.tipo == "VL")
                {                   
                    json.Votanti = afp.Votantitotali3.ToString();
                    json.Iscritti = afp.Iscritti.Iscrittitotaligen.ToString();
                    json.NumeroSezione = input.research.sezione;
                    json.Municipio = _sezioneService.findByNumerosezioneAndTipoelezioneId(int.Parse(input.research.sezione), tipoelezioneid).Municipio.ToString();
                    json.Tipo = input.research.tipo;
                    var sindaci = _sindacoService.findAllByTipoelezioneId(tipoelezioneid);
                    json.Sindaci = _votiLoader.ConvertToJsonSindaciEmpty(sindaci, input.research.sezione, input.research.tipo);
                }
                else
                {                  
                    json = _votiLoader.ConvertToJsonSindaci(votiLista, input.research.tipo,numerosindaci);
                    json.Iscritti = afp.Iscritti.Iscrittitotaligen.ToString();
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
            int numerosindaci = int.Parse(_elezioneConfig.Value.numerosindaci);
            try
            {

                int s = int.Parse(json.NumeroSezione);
                String msg = _businessRules.IsInsertable(s, json.Tipo, 0, tipoelezioneid);
                if (string.IsNullOrEmpty(msg))
                {
                    if (json.Tipo == "VL")
                    {
                        var voti = _votiLoader.prepareVoti(json, tipoelezioneid);
                        _votiSindacoService.CreateRange(voti);
                    }
                    if(json.Tipo == "RVL")
                    {
                        var voti = _votiLoader.prepareVoti(json, tipoelezioneid);
                        var votiOld = _votiSindacoService.findBySezioneNumerosezioneAndTipoelezioneId(int.Parse(json.NumeroSezione), tipoelezioneid);
                        List<VotiSindacoStorico> votiStorici =  _votiLoader.ConvertoToVotiSindacoOld(votiOld,numerosindaci);
                        _votiSindacoStoricoService.CreateRange(votiStorici);
                        _votiSindacoService.DeleteRange(votiOld);                        
                        _votiSindacoService.CreateRange(voti);
                    }
                    else if(json.Tipo != "VL" && json.Tipo != "RVL")
                    {
                        error.errMsg = "errore grave parametri non corretti";
                        return BadRequest(error);
                    }
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
            var ricalcolo = input.ricalcolo;
            int tipoelezioneid = int.Parse(_elezioneConfig.Value.tipoelezioneid);
            int ricalcoloLista = int.Parse(_elezioneConfig.Value.ricalcoloVotiLista);
            int ricalcoloSindaco = int.Parse(_elezioneConfig.Value.ricalcoloVotiSindaco);
            int totaleSezioni = int.Parse(_elezioneConfig.Value.totaleSezioni);
            try
            {

                switch (ricalcolo.Tipo.ToLower())
                {
                    case string a when a.Contains("lista") == true:
                        List<RicalcoloVotiLista> ricalcoloVotiListas = _votiLoader.ConvertToListeRicalcolo(json, tipoelezioneid, totaleSezioni, ricalcoloLista);
                        var ricalcoloVotiListasOld = _ricalcoloListaService.findByMunicipio(tipoelezioneid, ricalcoloVotiListas.FirstOrDefault().Municipio);
                        _ricalcoloListaService.DeleteRange(ricalcoloVotiListasOld);
                        _ricalcoloListaService.CreateRange(ricalcoloVotiListas);
                        break;
                    case string a when a.Contains("sindaco") == true:
                        List <RicalcoloVotiSindaco> ricalcoloVotiSindaco = _votiLoader.ConvertToSindacoRicalcolo(json, tipoelezioneid, totaleSezioni, ricalcoloSindaco);
                        var ricalcoloVotiSindacoOld = _ricalcoloSindacoService.findByMunicipio(tipoelezioneid, ricalcoloVotiSindaco.FirstOrDefault().Municipio);
                        _ricalcoloSindacoService.DeleteRange(ricalcoloVotiSindacoOld);
                        _ricalcoloSindacoService.CreateRange(ricalcoloVotiSindaco);
                        break;
                    default:
                        return  BadRequest("Attenzione parametri errati");
                        break;
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
    }
}
